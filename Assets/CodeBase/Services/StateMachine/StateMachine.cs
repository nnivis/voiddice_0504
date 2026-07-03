using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.StateMachine
{
    public class StateMachine
    {
        private readonly List<IStateMachine> _states = new();
        private IStateMachine _currentState;

        public StateMachine(IEnumerable<IStateMachine> states)
        {
            _states.AddRange(states);
            _states.ForEach(x => x.Init(this));
        }

        public void Change<T>() where T : IStateMachine
        {
            Change(typeof(T));
        }

        public void Change(Type typeNextState)
        {
            var nextState = _states.Find(x => x.GetType() == typeNextState);

            if (nextState == null)
            {
                Debug.LogError($"[StateMachine] Состояние {typeNextState.Name} не найдено! Зарегистрировано состояний: {_states.Count}");
                return;
            }

            if (_currentState == nextState)
            {
                Debug.LogWarning($"[StateMachine] Уже в состоянии {typeNextState.Name}");
                return;
            }

            Debug.Log($"[StateMachine] {_currentState?.GetType().Name ?? "none"} → {typeNextState.Name}");

            if (_currentState != null)
            {
                _currentState.Exit();
                foreach (var view in _currentState.GetViews())
                    view.SetActive(false);
            }

            try { nextState.Enter(); }
            catch (Exception e) { Debug.LogError($"[StateMachine] OnEnter {typeNextState.Name}: {e}"); }

            foreach (var view in nextState.GetViews())
                view.SetActive(true);

            _currentState = nextState;
        }

        public void Release()
        {
            if (_currentState != null)
            {
                _currentState.Exit();
                _currentState = null;
            }

            _states.Clear();
        }
    }
}
