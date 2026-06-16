using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.DataProvider;

namespace CodeBase.Domain.Location.View
{
    public class FirstLevelActivator : ILevelActivator
    {
        private PassedLevelChecker _passedLevelChecker;

        public FirstLevelActivator(PassedLevelChecker passedLevelChecker)
        {
            _passedLevelChecker = passedLevelChecker;
        }

        public void ActivateLevels(List<LevelView> levelViewsList, Location location)
        {
            List<int> passedLevels = GetPassedLevels(location);

            if (passedLevels.Count > 0)
            {
                ActivateLevelsToLastPassedLevel(levelViewsList, passedLevels);
                ActivatePassedLevels(levelViewsList, passedLevels);
            }
            else
            {
                if (levelViewsList.Count > 0)
                {
                    levelViewsList[0].ActiveState();
                    for (int i = 1; i < levelViewsList.Count; i++)
                        levelViewsList[i].InactiveState();
                }
            }
        }

        private List<int> GetPassedLevels(Location location)
        {
            List<int> passedLevels = new List<int>();

            for (int i = 0; i <= location.NumberofLevels; i++)
            {
                _passedLevelChecker.Visit(location.LocationType, i);
                if (_passedLevelChecker.IsLevelOpen)
                    passedLevels.Add(i);
            }

            return passedLevels;
        }

        private void ActivatePassedLevels(List<LevelView> levelViewsList, List<int> passedLevels)
        {
            foreach (int levelIndex in passedLevels)
                levelViewsList[levelIndex].PassedState();
        }

        private void ActivateLevelsToLastPassedLevel(List<LevelView> levelViewsList, List<int> passedLevels)
        {
            Dictionary<List<int>, List<int>> activationLogic = new Dictionary<List<int>, List<int>>
            {
                { new List<int> { 0 }, new List<int> { 1, 2 } },
                { new List<int> { 0, 1 }, new List<int> { 3, 2 } },
                { new List<int> { 0, 2 }, new List<int> { 1, 4 } },
                { new List<int> { 0, 1, 2 }, new List<int> { 3, 4 } },
                { new List<int> { 0, 1, 2, 3 }, new List<int> { 4 } },
                { new List<int> { 0, 1, 2, 4 }, new List<int> { 3 } },
                { new List<int> { 0, 1, 2, 3, 4 }, new List<int> { 5 } },
                { new List<int> { 0, 1, 2, 3, 4, 5 }, new List<int> { 6 } },
            };

            foreach (var kvp in activationLogic)
            {
                if (passedLevels.All(kvp.Key.Contains))
                {
                    foreach (var index in kvp.Value)
                        levelViewsList[index].ActiveState();

                    for (int i = 0; i < levelViewsList.Count; i++)
                        if (!kvp.Value.Contains(i))
                            levelViewsList[i].InactiveState();

                    return;
                }
            }

            foreach (var levelView in levelViewsList)
                levelView.InactiveState();
        }
    }
}
