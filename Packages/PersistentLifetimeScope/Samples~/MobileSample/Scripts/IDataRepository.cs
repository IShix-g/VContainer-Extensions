
namespace PersistentLifetimeScope.MobileSample
{
    public interface IDataRepository
    {
        DataModel Get();
        void Save();
    }
}
