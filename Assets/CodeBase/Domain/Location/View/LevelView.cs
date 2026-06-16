using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Domain.Location.View
{
    public class LevelView : MonoBehaviour
    {
        public Action<int> OnActiveLevelClicked;
        [SerializeField] Button _inactiveButton;
        [SerializeField] Button _activeButton;
        [SerializeField] Button _passedButton;
        private int _levelIndex;

        public void UpdateIndex(int levelIndex)
        {
            _levelIndex = levelIndex;
        }

        public void InactiveState()
        {
            _inactiveButton.gameObject.SetActive(true);
            _activeButton.gameObject.SetActive(false);
            _passedButton.gameObject.SetActive(false);
        }

        public void ActiveState()
        {
            _inactiveButton.gameObject.SetActive(false);
            _activeButton.gameObject.SetActive(true);
            _passedButton.gameObject.SetActive(false);
        }

        public void PassedState()
        {
            _inactiveButton.gameObject.SetActive(false);
            _activeButton.gameObject.SetActive(false);
            _passedButton.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            _activeButton.onClick.AddListener(OnActiveButtonClicked);
        }

        private void OnDisable()
        {
            _activeButton.onClick.RemoveListener(OnActiveButtonClicked);
        }

        private void OnActiveButtonClicked()
        {
            Debug.Log($"[LevelView] OnActiveButtonClicked - levelIndex={_levelIndex}, listeners={OnActiveLevelClicked?.GetInvocationList().Length ?? 0}");
            if (OnActiveLevelClicked != null)
                OnActiveLevelClicked(_levelIndex);
        }
    }
}
