using System;
using UnityEngine;
using CodeBase.Domain.Health;
using CodeBase.Services.GamePlay;

namespace CodeBase.Domain.Enemy
{
    public class Enemy : MonoBehaviour, IDamageable
    {
        public Action<GameFightEndReason> onDead;
        public GameFightEndReason GameOverType => _gameOverType;
        public Action onDamage;
        private HealthComponent _healthComponent;
        private GameFightEndReason _gameOverType;
        private EnemyConfig _config;
        [SerializeField] private ViewHealthComponent _viewComponent;

        public virtual void Initialize(EnemyConfig config)
        {
            _config = config;
            _healthComponent = new HealthComponent(config.MaxHealth);
            _gameOverType = GameFightEndReason.EnemyDeath;
        }

        public int CurrentHealth => _healthComponent.currentHealth;
        public int MaxHealth => _healthComponent.maxHealth;
        public int AttackDamage => _config.AttackDamage;

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
        }
    }
}
