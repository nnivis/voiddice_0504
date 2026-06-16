using UnityEngine;
using CodeBase.Domain.LevelBuild;
using CodeBase.Domain.Location;

namespace CodeBase.Services.GamePlay
{
    public class ProgressGameMediator : MonoBehaviour
    {
        [SerializeField] private LocationHandler _locationHandler;
        [SerializeField] private LevelProgressHandler _levelProgressHandler;

        public void ActiveLevel(int levelIndex)
        {
            Debug.Log($"[ProgressGameMediator] ActiveLevel({levelIndex}). _locationHandler null? {_locationHandler == null}");
            _locationHandler.ActiveLevel(levelIndex);
        }

        public void LevelComplete()
        {
            _locationHandler.PassLevel();
        }
    }
}
