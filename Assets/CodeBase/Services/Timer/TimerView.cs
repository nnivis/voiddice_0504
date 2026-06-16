using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Services.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Image _timerImage;
        private float _defaultTimer;
        private float _currentTimer;

        public void UpdateTimerInfo(float defaultTimer, float currentTimer)
        {
            _defaultTimer = defaultTimer;
            _currentTimer = currentTimer;
        }

        private void UpdateTimerView()
        {
            float timerPercentage = _currentTimer / _defaultTimer * 100f;
            _timerImage.fillAmount = timerPercentage / 100f;
        }

        private void Update() => UpdateTimerView();
    }
}
