using System;
using System.Linq;
using UnityEngine;

namespace CodeBase.Domain.Dice
{
    [CreateAssetMenu(fileName = "DiceFactory", menuName = "Factory/DiceFactory", order = 3)]
    public class DiceFactory : ScriptableObject, IDiceConfigProvider
    {
        [SerializeField] private DiceConfig _attackPlayer, _attackEnemy, _health;

        public Dice Get(DiceType type)
        {
            DiceConfig config = GetConfig(type);
            Dice instance = Instantiate(config.Prefab);
            instance.Initialize(this, config.Icon, config.Value, config.Type);
            return instance;
        }

        private DiceConfig GetConfig(DiceType type)
        {
            switch (type)
            {
                case DiceType.AttackPlayer: return _attackPlayer;
                case DiceType.AttackEnemy: return _attackEnemy;
                case DiceType.Health: return _health;
                default: throw new ArgumentException(nameof(type));
            }
        }

        public DiceType GetRandomType(DiceType currentType)
        {
            DiceType[] types = (DiceType[])Enum.GetValues(typeof(DiceType));
            types = types.Where(type => type != currentType).ToArray();
            return types.Length > 0
                ? types[UnityEngine.Random.Range(0, types.Length)]
                : currentType;
        }

        DiceConfig IDiceConfigProvider.GetConfig(DiceType type) => GetConfig(type);
    }
}
