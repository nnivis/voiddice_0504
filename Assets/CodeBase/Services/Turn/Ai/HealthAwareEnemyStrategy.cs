using System.Collections.Generic;
using CodeBase.Domain.Dice;
using CodeBase.Domain.Enemy;
using CodeBase.Services.GamePlay;

namespace CodeBase.Services.Turn.Ai
{
    // Аналог RandomAiMoveStrategy из sivin, но с health-awareness:
    // HP < порога → ищет Health кубик (лечит себя)
    // HP >= порога → ищет AttackPlayer кубик (атакует игрока)
    public class HealthAwareEnemyStrategy : IEnemyDiceStrategy
    {
        private readonly float _healThreshold;

        public HealthAwareEnemyStrategy(float healThreshold = 0.3f)
        {
            _healThreshold = healThreshold;
        }

        public Dice SelectAndApply(List<Dice> availableDice, Enemy enemy, AbilityMediator mediator)
        {
            if (availableDice == null || availableDice.Count == 0)
            {
                mediator.ApplyDamageToPlayer(enemy.AttackDamage);
                return null;
            }

            Dice chosen = PickDie(availableDice, enemy);
            Apply(chosen, mediator);
            return chosen;
        }

        private Dice PickDie(List<Dice> dice, Enemy enemy)
        {
            float healthPercent = (float)enemy.CurrentHealth / enemy.MaxHealth;
            DiceType preferred = healthPercent < _healThreshold
                ? DiceType.Health
                : DiceType.AttackPlayer;

            // Пробуем нужный тип
            Dice chosen = dice.Find(d => d.CurrentType == preferred);

            // Fallback: любой кубик кроме AttackEnemy (враг не атакует сам себя)
            if (chosen == null)
                chosen = dice.Find(d => d.CurrentType != DiceType.AttackEnemy);

            // Последний резерв: любой
            if (chosen == null)
                chosen = dice[0];

            return chosen;
        }

        private void Apply(Dice chosen, AbilityMediator mediator)
        {
            switch (chosen.CurrentType)
            {
                case DiceType.AttackPlayer:
                    mediator.ApplyDamageToPlayer(chosen.AbilityValue);
                    break;
                case DiceType.Health:
                    mediator.ApplyHealingToEnemy(chosen.AbilityValue);
                    break;
                case DiceType.AttackEnemy:
                    // враг игнорирует — кубик просто убирается
                    break;
            }
        }
    }
}
