using CodeBase.Domain.Location;

namespace CodeBase.Infrastructure.DataProvider
{
    public interface ILocationSelectedVisitor
    {
        void Visit(LocationType locationType);
    }
}
