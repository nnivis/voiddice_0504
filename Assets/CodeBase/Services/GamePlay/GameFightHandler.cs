using System.Collections.Generic;
using UnityEngine;
using VContainer;
using CodeBase.Domain.Character;
using CodeBase.Domain.Enemy;
using Dice = CodeBase.Domain.Dice.Dice;
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
        private AbilityMediator _abilityMediator;
        private Character _character;
        private Enemy _enemy;
        private List<Dice> _activeDice = new List<Dice>();

        [Inject]
        private void Construct(AbilityMediator abilityMediator)
        {
            _abilityMediator = abilityMediator;
            _diceSpawner.Initialize(_abilityMediator);
        }

        private void OnDisable()
        {
            _activeDice.ForEach(dice => dice.OnDestroyed -= DiceDestroyed);
        }

        private void SpawnComponents()
        {
            _character = _characterSpawner.SpawnCharacter();
            _enemy = _enemySpawner.SpawnEnemy();
            _diceSpawner.SpawnDice();
            _diceSpawner.SpawnFinished += AddSpawnedDiceToActiveDice;
        }

        private void DiceDestroyed()
        {
            _activeDice.RemoveAt(0);
            if (_activeDice.Count == 0)
            {
                _activeDice.Clear();
                _diceSpawner.SpawnDice();
                _diceSpawner.SpawnFinished += AddSpawnedDiceToActiveDice;
            }
        }

        private void AddSpawnedDiceToActiveDice()
        {
            _activeDice.AddRange(_diceSpawner.SpawnedDice);
            _activeDice.ForEach(dice => dice.OnDestroyed += DiceDestroyed);
            _diceSpawner.SpawnFinished -= AddSpawnedDiceToActiveDice;
        }

        private void ClearLevel()
        {
            Destroy(_character);
            Destroy(_enemy);
            _activeDice.Clear();
            _diceSpawner.RemoveAllChildren();
        }

        public void StartFight()
        {
            ClearLevel();
            SpawnComponents();
            _abilityMediator.SetComponent(_character, _enemy);
            _timer.StartTimer();
        }
    }
}
