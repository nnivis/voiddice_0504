using UnityEngine;

namespace CodeBase.Domain.Character
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "Config/CharacterConfig", order = 1)]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField, Range(0, 1000)] private int _maxHealth;

        public int MaxHealth => _maxHealth;
    }
}
