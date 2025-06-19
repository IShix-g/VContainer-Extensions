#if !ENABLE_VCONTAINER
using VContainer;
using VContainer.Unity.Extensions;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class AppLifetimeScope : PersistentLifetimeScope<AppLifetimeScope>
    {
        protected override void OnInitialize()
        {
            // This block runs only once
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDataRepository, PlayerPrefsDataRepository>(Lifetime.Singleton);
            builder.Register<IPreSave, OpenAppTimeTracker>(Lifetime.Singleton).AsSelf();
        }
    }
}
#endif
