using System;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    [Serializable]
    public class EnemyConfig
    {
        [SerializeField] private Enemy _prefab;
        [SerializeField] private int _health;
        [SerializeField] private int _attackDamage;

        public Enemy Prefab => _prefab;
        public int MaxHealth => _health;
        public int AttackDamage => _attackDamage;
    }
}
