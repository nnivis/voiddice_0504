using UnityEngine;
using CodeBase.Domain.LevelBuild;

namespace CodeBase.Services.StateMachine.States
{
    public class LevelBuildState : StateMachineBehavior
    {
        [SerializeField] private LevelProgressHandler _levelProgressHandler;

        protected override void OnEnter() => _levelProgressHandler.StartWork();
        protected override void OnExit() { }
    }
}
