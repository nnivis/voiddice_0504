using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using CodeBase.Domain.Location;

namespace CodeBase.Infrastructure.DataProvider
{
    public class PlayerData
    {
        private long _coins;
        private Dictionary<LocationType, List<int>> _passedLevels;
        private LocationType _selectedLocation;

        public PlayerData()
        {
            _coins = 3000;
            _passedLevels = new Dictionary<LocationType, List<int>>();
            _selectedLocation = LocationType.FirstLocation;
            AddPassedLevel(LocationType.FirstLocation, 0);
        }

        [JsonConstructor]
        public PlayerData(long coins, Dictionary<LocationType, List<int>> passedLevels, LocationType selectedLocation)
        {
            Coins = coins;
            _passedLevels = passedLevels ?? new Dictionary<LocationType, List<int>>();
            _selectedLocation = selectedLocation;
        }

        public long Coins
        {
            get => _coins;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                _coins = value;
            }
        }

        public LocationType SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if (!_passedLevels.ContainsKey(value))
                    throw new ArgumentException("Selected level is not passed");
                _selectedLocation = value;
            }
        }

        public Dictionary<LocationType, List<int>> PassedLevels => _passedLevels;

        public void AddPassedLevel(LocationType location, int level)
        {
            if (!_passedLevels.ContainsKey(location))
                _passedLevels[location] = new List<int>();

            if (_passedLevels[location].Contains(level))
                throw new ArgumentException("Level already passed");

            _passedLevels[location].Add(level);
        }
    }
}
