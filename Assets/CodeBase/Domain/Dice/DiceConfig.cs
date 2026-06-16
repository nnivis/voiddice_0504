using System;
using UnityEngine;

namespace CodeBase.Domain.Dice
{
    [Serializable]
    public class DiceConfig
    {
        [SerializeField] private Dice _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField, Range(0, 100)] private int _value;
        [SerializeField] private DiceType _type;

        public Dice Prefab => _prefab;
        public Sprite Icon => _icon;
        public int Value => _value;
        public DiceType Type => _type;
    }
}
