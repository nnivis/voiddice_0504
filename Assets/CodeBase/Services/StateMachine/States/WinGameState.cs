using UnityEngine;
using CodeBase.Services.GamePlay;
using CodeBase.UI;

namespace CodeBase.Services.StateMachine.States
{
    public class WinGameState : StateMachineBehavior
    {
        [SerializeField] private ProgressGameMediator _progressGameMediator;
        [SerializeField] private WinGameButton _winGameButton;

        protected override void OnEnter()
        {
            _progressGameMediator?.LevelComplete();
            if (_winGameButton != null)
                _winGameButton.OnWinGameClicked += GoToStartGame;
            else
                Debug.LogWarning("[WinGameState] _winGameButton не назначен в Inspector");
        }

        protected override void OnExit()
        {
            if (_winGameButton != null)
                _winGameButton.OnWinGameClicked -= GoToStartGame;
        }

        private void GoToStartGame() => stateMachine.Change<StartGameState>();
    }
}
