using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Domain.Location
{
    [CreateAssetMenu(fileName = "LocationContent", menuName = "Location/LocationContent")]
    public class LocationContent : ScriptableObject
    {
        [SerializeField] private List<Location> _locations;
        public IEnumerable<Location> Locations => _locations;

        public Location GetLocationByType(LocationType type)
        {
            return _locations.FirstOrDefault(location => location.LocationType == type);
        }

        private void OnValidate()
        {
            var duplicates = _locations.GroupBy(location => location.LocationType)
                .Where(array => array.Count() > 1);

            if (duplicates.Count() > 0)
                throw new InvalidOperationException(nameof(_locations));
        }
    }
}
