#if ENABLE_VCONTAINER
using VContainer;
using VContainer.Unity;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class SampleSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}
#endif
