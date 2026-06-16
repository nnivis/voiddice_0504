using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.Domain.LevelBuild.Block;
using CodeBase.Domain.LevelBuild.View;

namespace CodeBase.Domain.LevelBuild
{
    public class LevelProgressPanel : MonoBehaviour
    {
        public Action OnLevelBuild;
        public Action OnAnimationComplete;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private BlockViewFactory _blockBuildFactory;
        [SerializeField] private CharacterView _characterView;
        private BlockContent _blockContent;
        private List<BlockView> _blockViews = new List<BlockView>();

        public void Initialization(BlockContent blockContent)
        {
            _blockContent = blockContent;
        }

        private void SpawnBlocksOfType(BlockType blockType)
        {
            foreach (var block in _blockContent.Blocks)
                if (block.BlockType == blockType)
                    SpawnBlock(block);
        }

        private IEnumerator SpawnOtherBlocks(int numberOfLevel)
        {
            yield return new WaitForSeconds(0.5f);
            int spawnCount = 0;

            List<BlockConfig> otherBlocks = new List<BlockConfig>();
            foreach (var block in _blockContent.Blocks)
                if (block.BlockType != BlockType.Start && block.BlockType != BlockType.Finish)
                    otherBlocks.Add(block);

            while (spawnCount < numberOfLevel && otherBlocks.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, otherBlocks.Count);
                SpawnBlock(otherBlocks[randomIndex]);
                spawnCount++;
                yield return new WaitForSeconds(0.5f);
            }

            SpawnBlocksOfType(BlockType.Finish);
            StartCoroutine(SpawnrBlocks());
        }

        private void SpawnBlock(BlockConfig blockConfig)
        {
            BlockView blockView = _blockBuildFactory.Get(blockConfig, _spawnParent);
            _blockViews.Add(blockView);
        }

        private IEnumerator SpawnrBlocks()
        {
            yield return new WaitForSeconds(0.5f);
            InitializationCharacterView();
        }

        private void InitializationCharacterView()
        {
            DisableGridLayoutGroup();
            List<Vector3> blockPositions = _blockViews.Select(view => GetChildPositionRelativeToLayoutGroup(view.transform)).ToList();

            _characterView.gameObject.SetActive(true);
            _characterView.Initialization(blockPositions);
            _characterView.StartAnimation();
        }

        private void DisableGridLayoutGroup()
        {
            GridLayoutGroup gridLayout = _spawnParent.GetComponent<GridLayoutGroup>();
            gridLayout.enabled = false;
        }

        private Vector3 GetChildPositionRelativeToLayoutGroup(Transform child)
        {
            RectTransform childRectTransform = child.GetComponent<RectTransform>();
            return childRectTransform.anchoredPosition;
        }

        private void AnimationComplete()
        {
            StartCoroutine(AnimationCompleteDelay());
        }

        private IEnumerator AnimationCompleteDelay()
        {
            yield return new WaitForSeconds(0.5f);
            OnAnimationComplete();
        }

        public void Clear()
        {
            foreach (BlockView blockView in _blockViews)
                Destroy(blockView);

            _blockViews.Clear();
            _characterView.OnAnimationComplete -= AnimationComplete;
        }

        public void BuildLevel(int numberOfLevel)
        {
            Clear();

            SpawnBlocksOfType(BlockType.Start);
            StartCoroutine(SpawnOtherBlocks(numberOfLevel));
            _characterView.OnAnimationComplete += AnimationComplete;
        }

        public void MoveCharacterLevel(int currentLevel)
        {
            Debug.Log("Уровень" + currentLevel);
            _characterView.MoveToPosition(currentLevel);
        }
    }
}
