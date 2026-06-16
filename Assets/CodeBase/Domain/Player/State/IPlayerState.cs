using UnityEngine;

namespace CodeBase.Domain.Player.State
{
    public interface IPlayerState
    {
        void Enter();
        void Exit();
        void HandleLeftClick();
        void HandleRightClick();
        void HandleTriggerEnter2D(Collider2D collider);
        bool isDice();
    }
}
