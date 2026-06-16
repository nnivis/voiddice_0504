using UnityEngine;

namespace CodeBase.Domain.Player.State
{
    public class PassiveState : IPlayerState
    {
        private readonly GameObject _passivePrefab;

        public PassiveState(PlayerStateMachine playerStateMachine, GameObject passivePrefab)
        {
            _passivePrefab = passivePrefab;
        }

        public void Enter() => _passivePrefab.SetActive(true);
        public void Exit() => _passivePrefab.SetActive(false);

        public void HandleLeftClick() { }
        public void HandleRightClick() => Debug.Log("Passive");

        public void HandleTriggerEnter2D(Collider2D collider) { }

        public bool isDice() => true;
    }
}
