using CodeBase.Domain.Health;
using CodeBase.Domain.Dice;

namespace CodeBase.Services.GamePlay
{
    public class AbilityMediator
    {
        private IDamageable _character;
        private IDamageable _enemy;

        public bool IsPlayerInputEnabled { get; private set; } = true;

        public void SetComponent(IDamageable character, IDamageable enemy)
        {
            _character = character;
            _enemy = enemy;
        }

        public void SetPlayerInputEnabled(bool enabled) => IsPlayerInputEnabled = enabled;

        public void ApplyDamageToPlayer(int damage) => _character.ApplyDamage(damage);
        public void ApplyDamageToEnemy(int damage) => _enemy.ApplyDamage(damage);
        public void ApplyHealingToPlayer(int amount) => _character.ApplyHealing(amount);
        public void ApplyHealingToEnemy(int amount) => _enemy.ApplyHealing(amount);

        public void HandleDiceAbility(DiceType diceType, int value)
        {
            if (!IsPlayerInputEnabled) return;

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
