namespace CodeBase.Infrastructure.DataProvider
{
    public interface IDataProvider
    {
        void Save();
        bool TryLoad();
    }
}
