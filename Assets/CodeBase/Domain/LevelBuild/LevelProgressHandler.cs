using CodeBase.Domain.Location;
using CodeBase.Infrastructure;
using CodeBase.Infrastructure.SceneLoad;
using CodeBase.Services.GamePlay;
using UnityEngine;
using VContainer;

namespace CodeBase.Domain.LevelBuild
{
    public class LevelProgressHandler : MonoBehaviour
    {
        [SerializeField] private LevelProgressPanel _levelProgressPanel;
        [SerializeField] private RollDicePanel _rollDicePanel;
        [SerializeField] private global::CodeBase.Domain.LevelBuild.Block.BlockContent _blockContent;
        [SerializeField] private ProgressGameMediator _progressGameMediator;
        private const int _indexDefaultPartLevel = 2;
        private const int _defaultNumberOfPartLevel = 1;
        private int _currentNumberOfPartLevel;
        private int _numberOfPartLevel;
        private bool _isLevelBuild = false;
        private ILocationProvaider _locationProvaider;
        private TransitionSceneMediator _transitionSceneMediator;

        [Inject]
        private void Construct(ILocationProvaider locationProvaider, TransitionSceneMediator transitionSceneMediator)
        {
            _transitionSceneMediator = transitionSceneMediator;
            _locationProvaider = locationProvaider;
        }

        public void Initialize()
        {
            _levelProgressPanel.Initialization(_blockContent);
            _levelProgressPanel.OnLevelBuild += UpdateProgressLevle;
            _levelProgressPanel.OnAnimationComplete += TransitionLevel;

            _rollDicePanel.OnGenereteRollDiceDone += BuildLevel;
            _rollDicePanel.Initialize(_locationProvaider);
        }

        private void TransitionLevel()
        {
            _transitionSceneMediator.NotifyTransition(SceneType.GameFight);
        }

        private void BuildLevel(int numberOfPartLevel)
        {
            _isLevelBuild = true;
            _numberOfPartLevel = numberOfPartLevel + _indexDefaultPartLevel;

            ResetCurrentLevel();

            _rollDicePanel.gameObject.SetActive(false);
            _levelProgressPanel.gameObject.SetActive(true);
            _levelProgressPanel.BuildLevel(numberOfPartLevel);
        }

        private void ResetCurrentLevel()
        {
            _currentNumberOfPartLevel = _defaultNumberOfPartLevel;
        }

        private void ClearLevel()
        {
            ResetCurrentLevel();
            _levelProgressPanel.Clear();
            _rollDicePanel.Clear();
        }

        private void DestroyLevel()
        {
            _progressGameMediator.LevelComplete();
            _rollDicePanel.gameObject.SetActive(true);
            _levelProgressPanel.gameObject.SetActive(false);
            ClearLevel();
        }

        public void UpdateProgressLevle()
        {
            if (_currentNumberOfPartLevel == _numberOfPartLevel)
            {
                DestroyLevel();
                _transitionSceneMediator.NotifyTransition(SceneType.StartGame);
            }
            else
            {
                _currentNumberOfPartLevel++;
                _levelProgressPanel.MoveCharacterLevel(_currentNumberOfPartLevel);
            }
        }

        public void StartWork()
        {
            if (_isLevelBuild)
                UpdateProgressLevle();
        }
    }
}
