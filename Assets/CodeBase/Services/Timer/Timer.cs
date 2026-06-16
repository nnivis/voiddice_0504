using UnityEngine;
using VContainer;
using CodeBase.Services.GamePlay;

namespace CodeBase.Services.Timer
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private GameFightViewMediator _gameFightViewMediator;
        private const float _defaultTimer = 15f;
        private GamePlayMediator _gamePlayMediator;
        private GameFightEndReason _gameFightEndReason;
        private float _currentTimer;
        private bool _isTimerStart;
        public float CurrentTimer => _currentTimer;
        public float DefaultTimer => _defaultTimer;

        [Inject]
        private void Construct(GamePlayMediator gamePlayMediator)
        {
            _gamePlayMediator = gamePlayMediator;
            _gameFightEndReason = GameFightEndReason.TimeUp;
            _isTimerStart = false;
        }

        public void StartTimer()
        {
            _currentTimer = _defaultTimer;
            _isTimerStart = true;
        }

        private void Update()
        {
            if (_isTimerStart)
                UpdateTimer();
        }

        private void UpdateTimer()
        {
            if (_currentTimer > 0f)
            {
                _currentTimer -= Time.deltaTime;
                UpdateView();
                if (_currentTimer <= 0f)
                {
                    _currentTimer = 0f;
                    HandleTimerExpiration();
                    _isTimerStart = false;
                }
            }
        }

        private void UpdateView()
        {
            _gameFightViewMediator.UpdateTimerInfo(_defaultTimer, _currentTimer);
        }

        private void HandleTimerExpiration()
        {
            _gamePlayMediator.NotifyGameOver(_gameFightEndReason);
        }
    }
}
