
using VContainer;
using VContainer.Unity.Extensions;

namespace Tests
{
    public sealed class SamplePersistentChildLifetimeScope : PersistentChildLifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DisposableServiceA>(Lifetime.Singleton);
        }
    }
}
