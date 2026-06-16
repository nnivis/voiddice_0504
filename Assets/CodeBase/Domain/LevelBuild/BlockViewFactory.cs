using UnityEngine;
using CodeBase.Domain.LevelBuild.Block;
using CodeBase.Domain.LevelBuild.View;

namespace CodeBase.Domain.LevelBuild
{
    [CreateAssetMenu(fileName = "LevelBuildFactory", menuName = "LevelBuild/LevelBuildFactory", order = 3)]
    public class BlockViewFactory : ScriptableObject
    {
        [SerializeField] private BlockView _blockView;

        public BlockView Get(BlockConfig block, Transform parent)
        {
            BlockView instance = Instantiate(_blockView, parent);
            instance.Initialize(block);
            return instance;
        }
    }
}
