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
            _levelProgressHandler.ResetForNewLevel();
            _locationHandler.ActiveLevel(levelIndex);
        }

        public void LevelComplete() => _locationHandler.PassLevel();
    }
}
