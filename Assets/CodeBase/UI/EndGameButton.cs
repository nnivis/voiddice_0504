using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class EndGameButton : MonoBehaviour
    {
        public Action OnEndGameClicked;
        [SerializeField] private Button _endButton;

        private void OnEnable() => _endButton.onClick.AddListener(OnEndGameButtonClicked);
        private void OnDisable() => _endButton.onClick.RemoveListener(OnEndGameButtonClicked);

        private void OnEndGameButtonClicked() => OnEndGameClicked?.Invoke();
    }
}
