#if ENABLE_VCONTAINER
using VContainer;
using VContainer.Unity.Extensions;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class SampleSceneLifetimeScope : PersistentChildLifetimeScope<PersistentLifetimeScope.MobileSample.AppLifetimeScope>
    {
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}
#endif
