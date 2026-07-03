using UnityEngine;
using VContainer;
using CodeBase.Infrastructure.SceneLoad;
using CodeBase.Services.StateMachine;

namespace CodeBase.Infrastructure
{
    public class TransitionScene : MonoBehaviour
    {
        [SerializeField] MainSceneMode _mainSceneMode;
        [SerializeField] WipeController _wipeController;
        private TransitionSceneMediator _transitionSceneMediator;
        private SceneType _currentSceneType;

        [Inject]
        private void Construct(TransitionSceneMediator transitionSceneMediator)
        {
            _transitionSceneMediator = transitionSceneMediator;
            _transitionSceneMediator.OnTransitionScene += ChangeState;
            Debug.Log($"[TransitionScene] Construct called. Mediator hash: {transitionSceneMediator?.GetHashCode()}, Subscribed to OnTransitionScene");
        }

        private void OnEnable() => _wipeController.TransitionCompleted += OnTransitionCompleted;
        private void OnDestroy() => _wipeController.TransitionCompleted -= OnTransitionCompleted;

        private void ChangeState(SceneType sceneType)
        {
            _currentSceneType = sceneType;
            _wipeController.AnimateOut();
        }

        private void OnTransitionCompleted()
        {
            switch (_currentSceneType)
            {
                case SceneType.StartGame: _mainSceneMode.GotoStartGame(); break;
                case SceneType.GameFight: _mainSceneMode.GotoMainGameFight(); break;
                case SceneType.LevelProgress: _mainSceneMode.GotoLevelBuild(); break;
                case SceneType.Settings: _mainSceneMode.GotoSettings(); break;
                case SceneType.EndGame: _mainSceneMode.GotoEndGame(); break;
                case SceneType.WinGame: _mainSceneMode.GotoWinGame(); break;
                case SceneType.LevelAllFightsComplete: _mainSceneMode.GotoLevelAllFightsComplete(); break;
                default: Debug.LogWarning("Unknown scene type."); break;
            }
            _wipeController.AnimateIn();
        }
    }
}
