#if ENABLE_VCONTAINER
using VContainer;
using VContainer.Unity.Extensions;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class AppLifetimeScope : PersistentLifetimeScope<AppLifetimeScope>
    {
        protected override void OnInitialize()
        {
            // This block is executed only once and is executed before the initialization of the LifetimeScope.
        }

        protected override void OnEveryAwake(AppLifetimeScope instance)
        {
            // This block is executed every time Awake() is called and runs before the initialization of the LifetimeScope.
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IDataRepository, PlayerPrefsDataRepository>(Lifetime.Singleton);
            builder.Register<IPreSave, OpenAppTimeTracker>(Lifetime.Singleton).AsSelf();
        }
    }
}
#endif
