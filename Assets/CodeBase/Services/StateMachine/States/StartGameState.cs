using UnityEngine;
using CodeBase.Domain.Location;

namespace CodeBase.Services.StateMachine.States
{
    public class StartGameState : StateMachineBehavior
    {
        [SerializeField] private LocationHandler _locationHandler;

        protected override void OnEnter()
        {
            Debug.Log($"[StartGameState] OnEnter. _locationHandler null? {_locationHandler == null}");
            _locationHandler.StartWork();
        }

        protected override void OnExit() { }
    }
}
