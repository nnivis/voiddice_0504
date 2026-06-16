using CodeBase.Domain.Location;

namespace CodeBase.Infrastructure.DataProvider
{
    public class PassedLevelChecker : ILevelVisitor
    {
        private IPersistentData _persistentData;
        public bool IsLevelOpen { get; private set; }

        public PassedLevelChecker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(LocationType locationType, int level) =>
            IsLevelOpen = _persistentData.PlayerData.PassedLevels.ContainsKey(locationType)
                && _persistentData.PlayerData.PassedLevels[locationType].Contains(level);
    }
}
