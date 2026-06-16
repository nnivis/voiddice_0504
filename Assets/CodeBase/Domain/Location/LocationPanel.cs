using System;
using UnityEngine;
using CodeBase.Domain.Location.View;
using CodeBase.Infrastructure.DataProvider;
using CodeBase.Services.GamePlay;

namespace CodeBase.Domain.Location
{
    public class LocationPanel : MonoBehaviour
    {
        [SerializeField] private Transform _locationParent;
        [SerializeField] private ProgressGameMediator _progressGameMediator;
        private LocationView _locationView;
        private PassedLevelChecker _passedLevelChecker;
        private ILevelActivator _levelActivator;

        public void Initialize(PassedLevelChecker passedLevelChecker)
        {
            _passedLevelChecker = passedLevelChecker;
        }

        public void Show(Location location)
        {
            Debug.Log($"[LocationPanel] Show(location) - locationView count: {_locationView?.LevelViews?.Count ?? 0}");
            Clear();

            _locationView = SpawnLocationView(location);
            _levelActivator = GetLevelActivator(location.LocationType);
            _levelActivator.ActivateLevels(_locationView.LevelViews, location);

            _locationView.LevelViews.ForEach(levelView => levelView.OnActiveLevelClicked += ActiveLevel);
            Debug.Log($"[LocationPanel] Show finished. Subscribed to {_locationView.LevelViews.Count} LevelViews");
        }

        private void ActiveLevel(int levelIndex)
        {
            Debug.Log($"[LocationPanel] ActiveLevel({levelIndex}). _progressGameMediator null? {_progressGameMediator == null}");
            _progressGameMediator.ActiveLevel(levelIndex);
        }

        private void Clear()
        {
            if (_locationView != null)
                Destroy(_locationView.gameObject);
        }

        private LocationView SpawnLocationView(Location selectedLocation)
        {
            LocationView locationView = Instantiate(selectedLocation.LocationView, _locationParent);
            locationView.Initialize();
            locationView.transform.localScale = Vector3.one;
            return locationView;
        }

        private ILevelActivator GetLevelActivator(LocationType type)
        {
            switch (type)
            {
                case LocationType.FirstLocation:
                    return new FirstLevelActivator(_passedLevelChecker);
                default:
                    throw new ArgumentException(nameof(type));
            }
        }
    }
}
