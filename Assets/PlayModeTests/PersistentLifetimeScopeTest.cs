using System.Collections;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using Tests;

namespace VContainer.Tests.Unity
{
    public class PersistentLifetimeScopeTest
    {
        [UnityTest]
        public IEnumerator Create()
        {
            var parentLifetimeScope = Create<SamplePersistentLifetimeScope>("Parent");

            yield return null;
            yield return null;

            var childLifetimeScope = Create<SamplePersistentChildLifetimeScope>("Child");

            yield return null;
            yield return null;

            var parentDisposableA = parentLifetimeScope.Container.Resolve<DisposableServiceA>();
            var parentDisposableB = parentLifetimeScope.Container.Resolve<DisposableServiceB>();
            var childDisposableA = childLifetimeScope.Container.Resolve<DisposableServiceA>();
            var childDisposableB = parentLifetimeScope.Container.Resolve<DisposableServiceB>();

            Assert.That(parentDisposableB, Is.SameAs(childDisposableB));

            // Scene reload
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);

            yield return null;
            yield return null;

            Assert.That(SceneManager.GetActiveScene().name, Is.EqualTo(sceneName));
            Assert.That(parentLifetimeScope != null, Is.True);
            Assert.That(childLifetimeScope == null, Is.True);
            Assert.That(parentDisposableA.Disposed, Is.False);
            Assert.That(childDisposableA.Disposed, Is.True);
            Assert.That(parentDisposableB.Disposed, Is.False);
            Assert.That(childDisposableB.Disposed, Is.False);
        }

        T Create<T>(string name) where T : Component
        {
            var obj = new GameObject(name);
            obj.SetActive(false);
            var component = obj.AddComponent<T>();
            obj.SetActive(true);
            return component;
        }
    }
}
