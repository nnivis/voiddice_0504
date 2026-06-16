using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CodeBase.Domain.LevelBuild;

namespace CodeBase.Domain.LevelBuild.Block
{
    [CreateAssetMenu(fileName = "BlockContent", menuName = "LevelBuild/BlockContent", order = 2)]
    public class BlockContent : ScriptableObject
    {
        [SerializeField] private List<BlockConfig> _bloks;
        public IEnumerable<BlockConfig> Blocks => _bloks;

        private void OnValidate()
        {
            var heroDuplicates = _bloks.GroupBy(block => block.BlockType)
                .Where(array => array.Count() > 1);

            if (heroDuplicates.Count() > 0)
                throw new InvalidOperationException(nameof(_bloks));
        }
    }
}
