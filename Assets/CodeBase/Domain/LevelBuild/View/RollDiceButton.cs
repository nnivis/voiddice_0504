using System;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.UI.Animation;

namespace CodeBase.Domain.LevelBuild.View
{
    public class RollDiceButton : MonoBehaviour
    {
        public Action OnActiveRollDiceClicked;
        [SerializeField] private Button _rollDiceButton;
        private SmoothScaleAnimation _smoothScaleAnimation;

        private void Awake()
        {
            _smoothScaleAnimation = new SmoothScaleAnimation(transform as RectTransform);
        }

        private void OnEnable()
        {
            _smoothScaleAnimation.StartAnimation();
            _rollDiceButton.onClick.AddListener(OnActiveRollDiceButtonClicked);
        }

        private void OnDisable()
        {
            _smoothScaleAnimation.StopAnimation();
            _rollDiceButton.onClick.RemoveListener(OnActiveRollDiceButtonClicked);
        }

        private void OnActiveRollDiceButtonClicked()
        {
            if (OnActiveRollDiceClicked != null)
                OnActiveRollDiceClicked();
        }

        public void DeactivateRollDiceButton() => gameObject.SetActive(false);
        public void ActiveRollDiceButton() => gameObject.SetActive(true);
    }
}
