using CodeBase.Domain.Location;

namespace CodeBase.Infrastructure.DataProvider
{
    public class SelectedLocationChecker : ILocationSelectedVisitor
    {
        private IPersistentData _persistentData;
        public bool IsSelected { get; private set; }

        public SelectedLocationChecker(IPersistentData persistentData) => _persistentData = persistentData;

        public void Visit(LocationType locationType) =>
            IsSelected = _persistentData.PlayerData.SelectedLocation == locationType;
    }
}
