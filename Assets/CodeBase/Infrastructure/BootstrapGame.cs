using UnityEngine;
using CodeBase.Domain.LevelBuild;
using CodeBase.Domain.Location;
using CodeBase.Infrastructure.DataProvider;

namespace CodeBase.Infrastructure
{
    public class BootstrapGame : MonoBehaviour
    {
        [SerializeField] FileDeletionExample _fileDeletionExample;
        [SerializeField] LocationHandler _locationHandler;
        [SerializeField] LevelProgressHandler _levelProgressHandler;
        private IDataProvider _dataProvider;
        private IPersistentData _persistentPlayertData;

        private void Awake()
        {
            _fileDeletionExample.DeleteJsonFile();
            InitializeData();
            InitializeLocation();
            InitializeLevel();
        }

        private void InitializeData()
        {
            _persistentPlayertData = new PersistentData();
            _dataProvider = new DataLocalProvider(_persistentPlayertData);
            LoadDataOrInit();
        }

        private void InitializeLocation()
        {
            PassedLevelChecker passedLevelChecker = new PassedLevelChecker(_persistentPlayertData);
            LevelPasser levelPasser = new LevelPasser(_persistentPlayertData);
            SelectedLocationChecker selectedLocationChecker = new SelectedLocationChecker(_persistentPlayertData);
            LocationSelector locationSelector = new LocationSelector(_persistentPlayertData);

            _locationHandler.Initialize(_dataProvider, passedLevelChecker, levelPasser, selectedLocationChecker, locationSelector);
        }

        private void InitializeLevel()
        {
            _levelProgressHandler.Initialize();
        }

        private void LoadDataOrInit()
        {
            if (_dataProvider.TryLoad() == false)
                _persistentPlayertData.PlayerData = new PlayerData();
        }
    }
}
