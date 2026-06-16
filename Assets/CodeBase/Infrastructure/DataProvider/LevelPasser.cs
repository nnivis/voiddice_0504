using CodeBase.Domain.Location;

namespace CodeBase.Infrastructure.DataProvider
{
    public class LevelPasser : ILevelVisitor
    {
        private IPersistentData _persistentData;

        public LevelPasser(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(LocationType locationType, int level) =>
            _persistentData.PlayerData.AddPassedLevel(locationType, level);
    }
}
