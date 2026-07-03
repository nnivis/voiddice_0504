using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using CodeBase.Domain.Dice;
using CodeBase.Domain.Enemy;
using CodeBase.Services.GamePlay;
using Object = UnityEngine.Object;

namespace CodeBase.Services.Turn.Ai
{
    // Аналог AiController из sivin.
    // Задержка передаётся снаружи как Func<float, Task> чтобы использовать
    // корутину Unity вместо Task.Delay (Task.Delay ненадёжен на main thread в Unity).
    public class EnemyFightController : IFightController
    {
        private readonly Enemy _enemy;
        private readonly AbilityMediator _abilityMediator;
        private readonly List<Dice> _availableDice;
        private readonly IEnemyDiceStrategy _strategy;
        private readonly float _thinkDelaySec;
        private readonly Func<float, Task> _delayFunc;

        public EnemyFightController(
            Enemy enemy,
            AbilityMediator abilityMediator,
            List<Dice> availableDice,
            IEnemyDiceStrategy strategy,
            float thinkDelaySec,
            Func<float, Task> delayFunc)
        {
            _enemy = enemy;
            _abilityMediator = abilityMediator;
            _availableDice = availableDice;
            _strategy = strategy;
            _thinkDelaySec = thinkDelaySec;
            _delayFunc = delayFunc;
        }

        public async Task RequestTurnAsync(CancellationToken ct)
        {
            Debug.Log($"[EnemyTurn] START — кубиков на поле: {_availableDice.Count}");
            _abilityMediator.SetPlayerInputEnabled(false);
            try
            {
                await _delayFunc(_thinkDelaySec);
                if (ct.IsCancellationRequested)
                {
                    Debug.Log("[EnemyTurn] отменён (бой завершился во время паузы)");
                    return;
                }

                Dice chosen = _strategy.SelectAndApply(_availableDice, _enemy, _abilityMediator);

                if (chosen != null)
                {
                    Debug.Log($"[EnemyTurn] выбран кубик {chosen.CurrentType} value={chosen.AbilityValue}");
                    _availableDice.Remove(chosen);
                    Object.Destroy(chosen.gameObject);
                }
                else
                {
                    Debug.Log($"[EnemyTurn] нет кубиков — fallback. AttackDamage={_enemy.AttackDamage}");
                }
            }
            finally
            {
                _abilityMediator.SetPlayerInputEnabled(true);
                Debug.Log("[EnemyTurn] END");
            }
        }
    }
}
