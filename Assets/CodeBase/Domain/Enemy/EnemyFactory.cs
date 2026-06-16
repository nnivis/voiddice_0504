using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Domain.Enemy
{
    [CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factory/EnemyFactory", order = 3)]
    public class EnemyFactory : ScriptableObject
    {
        [SerializeField] private List<EnemyConfig> _configs;

        public Enemy Get()
        {
            EnemyConfig config = GetConfig();
            Enemy enemy = Instantiate(config.Prefab);
            enemy.Initialize(config);
            return enemy;
        }

        private EnemyConfig GetConfig()
        {
            if (_configs.Count == 0)
                throw new ArgumentException("The List is empty");

            int randomIndex = UnityEngine.Random.Range(0, _configs.Count);
            return _configs[randomIndex];
        }
    }
}
