using UnityEngine;
using CodeBase.UI;

namespace CodeBase.Services.StateMachine.States
{
    public class EndGameState : StateMachineBehavior
    {
        [SerializeField] private EndGameButton _endGameButton;

        protected override void OnEnter()
        {
            if (_endGameButton != null)
                _endGameButton.OnEndGameClicked += GoToMenu;
            else
                Debug.LogWarning("[EndGameState] _endGameButton не назначен в Inspector");
        }

        protected override void OnExit()
        {
            if (_endGameButton != null)
                _endGameButton.OnEndGameClicked -= GoToMenu;
        }

        private void GoToMenu() => stateMachine.Change<StartGameState>();
    }
}
