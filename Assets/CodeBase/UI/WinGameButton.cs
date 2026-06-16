using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class WinGameButton : MonoBehaviour
    {
        public Action OnWinGameClicked;
        [SerializeField] private Button _winButton;

        private void OnEnable() => _winButton.onClick.AddListener(OnActiveRollDiceButtonClicked);
        private void OnDisable() => _winButton.onClick.RemoveListener(OnActiveRollDiceButtonClicked);

        private void OnActiveRollDiceButtonClicked()
        {
            if (OnWinGameClicked != null)
                OnWinGameClicked();
        }
    }
}
