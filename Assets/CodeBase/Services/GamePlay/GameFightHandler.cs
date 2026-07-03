using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using CodeBase.Domain.Character;
using CodeBase.Domain.Dice;
using CodeBase.Domain.Enemy;
using CodeBase.Services.Flow;
using CodeBase.Services.StateMachine;
using CodeBase.Services.Turn;
using CodeBase.Services.Turn.Ai;
using DiceSpawner = CodeBase.Domain.Dice.DiceSpawner;
using TimerService = CodeBase.Services.Timer.Timer;

namespace CodeBase.Services.GamePlay
{
    public class GameFightHandler : MonoBehaviour
    {
        [SerializeField] private DiceSpawner _diceSpawner;
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private TimerService _timer;
        [SerializeField] private float _enemyThinkDelay = 0.5f;
        [SerializeField, Range(0f, 1f)] private float _enemyHealThreshold = 0.3f;

        private AbilityMediator _abilityMediator;
        private GamePlayMediator _gamePlayMediator;
        private MainSceneMode _mainSceneMode;

        private Character _character;
        private Enemy _enemy;
        private readonly List<Dice> _activeDice = new List<Dice>();

        private PlayerFightController _playerController;
        private FightFlow _fightFlow;
        private bool _isFightActive;
        private bool _isEnemyActing;

        [Inject]
        private void Construct(AbilityMediator abilityMediator, GamePlayMediator gamePlayMediator, MainSceneMode mainSceneMode)
        {
            _abilityMediator = abilityMediator;
            _gamePlayMediator = gamePlayMediator;
            _mainSceneMode = mainSceneMode;
            _diceSpawner.Initialize(_abilityMediator);
        }

        private void OnEnable()
        {
            _gamePlayMediator.OnTurnExpired += OnTimerExpired;
            _gamePlayMediator.OnGameOver += OnFightEnded;
        }

        private void OnDisable()
        {
            _gamePlayMediator.OnTurnExpired -= OnTimerExpired;
            _gamePlayMediator.OnGameOver -= OnFightEnded;
            UnsubscribeFromDice();
        }

        // ── Entry Point ──────────────────────────────────────────────────────────

        public void StartFight()
        {
            if (_isFightActive)
            {
                Debug.LogWarning("[GameFightHandler] StartFight вызван пока бой активен — игнорируем");
                return;
            }

            _fightFlow?.Stop();
            ClearLevel();
            SpawnComponents();
            _isFightActive = true;
            _isEnemyActing = false;

            _playerController = new PlayerFightController();

            var strategy = new HealthAwareEnemyStrategy(_enemyHealThreshold);
            var enemyController = new EnemyFightController(
                _enemy, _abilityMediator, _activeDice, strategy, _enemyThinkDelay,
                delayFunc: CoroutineDelay);

            var turnSystem = new FightTurnSystem(
                playerController:  _playerController,
                enemyController:   enemyController,
                onPlayerTurnStart: () => _abilityMediator.SetPlayerInputEnabled(true),
                onEnemyTurnStart:  () => _isEnemyActing = true,
                onEnemyTurnEnd:    OnEnemyTurnEnd,
                isFinished:        () => !_isFightActive);

            _fightFlow = new FightFlow(turnSystem);

            SpawnDiceSet();
            _timer.StartTimer(); // таймер матча — запускается один раз
            _fightFlow.Start();
        }

        // ── Dice Set ─────────────────────────────────────────────────────────────

        // Спавн кубиков (таймер НЕ трогаем — он запущен один раз на весь матч)
        private void SpawnDiceSet()
        {
            _diceSpawner.DiceSpawned += OnSingleDiceSpawned;
            _diceSpawner.SpawnDice();
        }

        private void RespawnDice()
        {
            if (!_isFightActive) return;
            UnsubscribeFromDice();
            _activeDice.Clear();
            _diceSpawner.StopWork();
            _diceSpawner.RemoveAllChildren();
            SpawnDiceSet();
        }

        private IEnumerator RespawnAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            RespawnDice();
        }

        // ── Enemy Turn Callbacks ─────────────────────────────────────────────────

        private void OnEnemyTurnEnd()
        {
            _isEnemyActing = false;

            // Все кубики использованы → новая партия через 0.5 сек
            if (_activeDice.Count == 0 && _isFightActive)
                StartCoroutine(RespawnAfterDelay(0.5f));
        }

        // ── Timer = таймер матча, истёк → игрок проиграл ────────────────────────

        private void OnTimerExpired()
        {
            if (!_isFightActive) return;
            _gamePlayMediator.NotifyGameOver(GameFightEndReason.TimeUp);
        }

        // ── Dice Events ──────────────────────────────────────────────────────────

        private void OnSingleDiceSpawned(Dice dice)
        {
            _activeDice.Add(dice);
            dice.OnDestroyed += OnDiceDestroyed;
        }

        private void OnDiceDestroyed(Dice dice)
        {
            dice.OnDestroyed -= OnDiceDestroyed;
            _activeDice.Remove(dice);

            // Игрок использовал 1 кубик → сразу передаём ход врагу
            _playerController?.CompleteTurn();
        }

        private void UnsubscribeFromDice()
        {
            foreach (var dice in _activeDice)
                dice.OnDestroyed -= OnDiceDestroyed;
            _diceSpawner.DiceSpawned -= OnSingleDiceSpawned;
        }

        // ── Coroutine Delay ──────────────────────────────────────────────────────

        private Task CoroutineDelay(float seconds)
        {
            var tcs = new TaskCompletionSource<bool>();
            StartCoroutine(WaitCoroutine(seconds, tcs));
            return tcs.Task;
        }

        private IEnumerator WaitCoroutine(float seconds, TaskCompletionSource<bool> tcs)
        {
            yield return new WaitForSeconds(seconds);
            tcs.TrySetResult(true);
        }

        // ── Fight Events ─────────────────────────────────────────────────────────

        private void OnFightEnded(GameFightEndReason reason)
        {
            _isFightActive = false;
            _fightFlow?.Stop();
            StopAllCoroutines();

            if (reason == GameFightEndReason.EnemyDeath)
                _mainSceneMode.GotoWinGame();
            else
                _mainSceneMode.GotoEndGame();
        }

        // ── Lifecycle ────────────────────────────────────────────────────────────

        private void SpawnComponents()
        {
            _character = _characterSpawner.SpawnCharacter();
            _enemy = _enemySpawner.SpawnEnemy();
            _abilityMediator.SetComponent(_character, _enemy);
        }

        private void ClearLevel()
        {
            if (_character != null) Destroy(_character.gameObject);
            if (_enemy != null) Destroy(_enemy.gameObject);
            UnsubscribeFromDice();
            _activeDice.Clear();
            _diceSpawner.StopWork();
            _diceSpawner.RemoveAllChildren();
        }
    }
}
