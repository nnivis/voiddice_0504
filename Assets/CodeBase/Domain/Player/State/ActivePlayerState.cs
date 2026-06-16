using UnityEngine;

namespace CodeBase.Domain.Player.State
{
    public class ActivePlayerState : IPlayerState
    {
        private readonly GameObject _activePrefab;
        private CodeBase.Domain.Dice.Dice _dice;
        private bool _isDice;

        public ActivePlayerState(PlayerStateMachine playerStateMachine, GameObject activePrefab)
        {
            _activePrefab = activePrefab;
        }

        public void Enter() => _activePrefab.SetActive(true);
        public void Exit() => _activePrefab.SetActive(false);

        public void HandleLeftClick()
        {
            if (_dice != null)
                _dice.OnMassegeDiceLeftClick();
        }

        public void HandleRightClick()
        {
            if (_dice != null)
                _dice.OnMassegeDiceRightClick();
        }

        public void HandleTriggerEnter2D(Collider2D collider)
        {
            CodeBase.Domain.Dice.Dice diceComponent = collider.gameObject.GetComponent<CodeBase.Domain.Dice.Dice>();
            CodeBase.Domain.Hedge.Hedge hedgeComponent = collider.gameObject.GetComponent<CodeBase.Domain.Hedge.Hedge>();

            if (hedgeComponent != null)
            {
                _isDice = false;
                return;
            }
            else if (diceComponent != null)
            {
                _dice = diceComponent;
            }

            _isDice = true;
        }

        public bool isDice() => _isDice;
    }
}
