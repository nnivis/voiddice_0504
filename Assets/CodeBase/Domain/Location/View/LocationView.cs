using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Domain.Location.View
{
    public class LocationView : MonoBehaviour
    {
        [SerializeField] private List<LevelView> _levelViewsList;
        public List<LevelView> LevelViews => _levelViewsList;

        public void Initialize()
        {
            SetLevelIndex();
        }

        private void SetLevelIndex()
        {
            int index = 0;
            foreach (LevelView levelView in _levelViewsList)
            {
                levelView.UpdateIndex(index);
                index++;
            }
        }
    }
}
