using UnityEngine;
using CodeBase.Domain.LevelBuild.View;

namespace CodeBase.Domain.RollDice
{
    [CreateAssetMenu(fileName = "RollDice", menuName = "Config/RollDiceConfig", order = 3)]
    public class RollDiceConfig : ScriptableObject
    {
        [SerializeField] private RollDiceView _rollDice;
        [SerializeField] private int _indexOfFaces;
        [SerializeField] private Sprite _background;
        [SerializeField] private GameObject _iconIndexPrefab;

        public RollDiceView RollDice => _rollDice;
        public int IndexOfFaces => _indexOfFaces;
        public Sprite Background => _background;
        public GameObject IconIndexPrefab => _iconIndexPrefab;
    }
}
