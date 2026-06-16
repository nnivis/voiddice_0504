using UnityEngine;
using CodeBase.Services.GamePlay;

namespace CodeBase.Services.StateMachine.States
{
    public class GameFightState : StateMachineBehavior
    {
        [SerializeField] private GameFightHandler _gameFightHandler;

        protected override void OnEnter() => _gameFightHandler.StartFight();
        protected override void OnExit() { }
    }
}
