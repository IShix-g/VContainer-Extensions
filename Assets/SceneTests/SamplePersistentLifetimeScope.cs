
using VContainer;
using VContainer.Unity.Extensions;

namespace Tests
{
    public sealed class SamplePersistentLifetimeScope : PersistentLifetimeScope<SamplePersistentLifetimeScope>
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<DisposableServiceA>(Lifetime.Scoped);
            builder.Register<DisposableServiceB>(Lifetime.Singleton);
        }
    }
}
