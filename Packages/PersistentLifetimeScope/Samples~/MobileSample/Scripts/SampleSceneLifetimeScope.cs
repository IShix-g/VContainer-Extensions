#if ENABLE_VCONTAINER
using VContainer;
using VContainer.Unity.Extensions;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class SampleSceneLifetimeScope : PersistentChildLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}
#endif
