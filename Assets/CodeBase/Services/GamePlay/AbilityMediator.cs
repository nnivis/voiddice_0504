using CodeBase.Domain.Health;
using CodeBase.Domain.Dice;

namespace CodeBase.Services.GamePlay
{
    public class AbilityMediator
    {
        private IDamageable _character;
        private IDamageable _enemy;

        public void SetComponent(IDamageable character, IDamageable enemy)
        {
            _character = character;
            _enemy = enemy;
        }

        public void ApplyDamageToPlayer(int damage) => _enemy.ApplyDamage(damage);
        public void ApplyDamageToEnemy(int damage) => _character.ApplyDamage(damage);
        public void ApplyHealingToPlayer(int amount) => _character.ApplyHealing(amount);

        public void HandleDiceAbility(DiceType diceType, int value)
        {
            switch (diceType)
            {
                case DiceType.AttackEnemy:
                    ApplyDamageToEnemy(value);
                    break;
                case DiceType.AttackPlayer:
                    ApplyDamageToPlayer(value);
                    break;
                case DiceType.Health:
                    ApplyHealingToPlayer(value);
                    break;
            }
        }
    }
}
