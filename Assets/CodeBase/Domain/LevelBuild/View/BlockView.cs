using UnityEngine;
using UnityEngine.UI;
using CodeBase.Domain.LevelBuild.Block;

namespace CodeBase.Domain.LevelBuild.View
{
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private Image _eventIcon;
        [SerializeField] private Image _blockIcon;
        private BlockType _blockType;

        public void Initialize(BlockConfig blockConfig)
        {
            _blockIcon.sprite = blockConfig.BlockIcon;
            _blockType = blockConfig.BlockType;

            if (blockConfig.EventIcon != null)
                _eventIcon.sprite = blockConfig.EventIcon;
            else
                _eventIcon.gameObject.SetActive(false);
        }
    }
}
