using CodeBase.Domain.Location;

namespace CodeBase.Infrastructure.DataProvider
{
    public interface ILevelVisitor
    {
        void Visit(LocationType locationType, int level);
    }
}
