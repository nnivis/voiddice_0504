using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using CodeBase.Services.GamePlay;
using CodeBase.Services.Turn;

namespace CodeBase.Services.Flow
{
    // Аналог TurnSystem из sivin — чередует ходы игрока и врага,
    // вызывает callbacks для подготовки/очистки сцены между ходами
    public class FightTurnSystem
    {
        private readonly IFightController _playerController;
        private readonly IFightController _enemyController;
        private readonly Action _onPlayerTurnStart;  // включить ввод игрока
        private readonly Action _onEnemyTurnStart;   // враг начинает ход
        private readonly Action _onEnemyTurnEnd;     // враг закончил ход
        private readonly Func<bool> _isFinished;

        private TurnPhase _currentPhase = TurnPhase.PlayerTurn;
        public TurnPhase CurrentPhase => _currentPhase;

        public FightTurnSystem(
            IFightController playerController,
            IFightController enemyController,
            Action onPlayerTurnStart,
            Action onEnemyTurnStart,
            Action onEnemyTurnEnd,
            Func<bool> isFinished)
        {
            _playerController = playerController;
            _enemyController = enemyController;
            _onPlayerTurnStart = onPlayerTurnStart;
            _onEnemyTurnStart = onEnemyTurnStart;
            _onEnemyTurnEnd = onEnemyTurnEnd;
            _isFinished = isFinished;
        }

        public async Task PlayTurnAsync(CancellationToken ct)
        {
            if (_isFinished() || ct.IsCancellationRequested) return;

            if (_currentPhase == TurnPhase.PlayerTurn)
            {
                _onPlayerTurnStart?.Invoke();
                await _playerController.RequestTurnAsync(ct);

                if (!ct.IsCancellationRequested && !_isFinished())
                    _currentPhase = TurnPhase.EnemyTurn;
            }
            else
            {
                _onEnemyTurnStart?.Invoke();
                await _enemyController.RequestTurnAsync(ct);

                if (!ct.IsCancellationRequested)
                {
                    _onEnemyTurnEnd?.Invoke();
                    if (!_isFinished())
                        _currentPhase = TurnPhase.PlayerTurn;
                }
            }
        }
    }
}
