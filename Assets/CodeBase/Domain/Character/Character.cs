using System;
using UnityEngine;
using CodeBase.Domain.Health;
using CodeBase.Services.GamePlay;

namespace CodeBase.Domain.Character
{
    public class Character : MonoBehaviour, IDamageable
    {
        public Action<GameFightEndReason> onDead;
        public GameFightEndReason GameOverType => _gameOverType;
        public Action onDamage;
        [SerializeField] private ViewHealthComponent _viewComponent;
        private HealthComponent _healthComponent;
        private const GameFightEndReason _gameOverType = GameFightEndReason.PlayerDeath;

        public void Initialization(CharacterConfig config)
        {
            _healthComponent = new HealthComponent(config.MaxHealth);
        }

        public int CurrentHealth => _healthComponent.currentHealth;
        public int MaxHealth => _healthComponent.maxHealth;

        public void ApplyDamage(int damage)
        {
            _healthComponent.ReduceHealth(damage);
            _viewComponent.UpdateHealth(CurrentHealth, MaxHealth);
            onDamage?.Invoke();

            if (_healthComponent.isDead) Death();
        }

        public void ApplyHealing(int amount)
        {
            _healthComponent.IncreaseHealth(amount);
            _viewComponent.UpdateHealth(CurrentHealth, MaxHealth);
        }

        private void Death()
        {
            onDead?.Invoke(_gameOverType);
            Destroy(gameObject);
        }
    }
}
