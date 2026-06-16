using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Domain.Player.State
{
    public class PlayerStateMachine : IStateSwitcher
    {
        private List<IPlayerState> _states;
        private IPlayerState _currentState;
        public IPlayerState CurrentState => _currentState;

        public PlayerStateMachine(GameObject passivePrefab, GameObject activePrefab)
        {
            _states = new List<IPlayerState>
            {
                new PassiveState(this, passivePrefab),
                new ActivePlayerState(this, activePrefab),
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<T>() where T : IPlayerState
        {
            IPlayerState state = _states.FirstOrDefault(state => state is T);

            _currentState.Exit();
            _currentState = state;
            _currentState.Enter();
        }
    }
}
