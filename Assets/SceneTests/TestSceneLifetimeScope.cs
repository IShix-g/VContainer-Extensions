
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity.Extensions;

namespace Tests
{
    public sealed class TestSceneLifetimeScope : PersistentChildLifetimeScope<SamplePersistentLifetimeScope>
    {
        static int s_instanceCount;
        static DisposableServiceB s_serviceB;

        void Start()
        {
            _ = Reload();
        }

        async Task Reload()
        {
            Debug.Log("Reload count = " + ++s_instanceCount);
            await Task.Delay(3000);
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterBuildCallback(container =>
            {
                var serviceB = container.Resolve<DisposableServiceB>();
                Assert.IsTrue(s_serviceB == null || s_serviceB == serviceB, "Instance is different.");
                s_serviceB = serviceB;

            });
        }
    }
}
