#if ENABLE_VCONTAINER
using UnityEngine;

namespace VContainer.Unity.Extensions
{
    public abstract class PersistentChildLifetimeScope : PersistentChildLifetimeScopeBase {}

    public abstract class PersistentChildLifetimeScope<T> : PersistentChildLifetimeScopeBase where T : PersistentLifetimeScope<T>
    {
        protected void OnValidate()
        {
            if (parentReference.Type != typeof(T))
            {
                Reset();
            }
        }

        protected virtual void Reset()
        {
            parentReference = ParentReference.Create<T>();
            autoRun = true;
        }
    }

    [HelpURL("https://github.com/IShix-g/VContainer-Extensions?tab=readme-ov-file#persistentlifetimescope")]
    public abstract class PersistentChildLifetimeScopeBase : LifetimeScope
    {
#if UNITY_2020_3 || UNITY_2021_3 || UNITY_2022_2 || UNITY_2022_3_OR_NEWER
        protected override LifetimeScope FindParent()
        {
            if (parentReference.Type == null)
            {
                return null;
            }

            var objs = FindObjectsByType(
                parentReference.Type,
                FindObjectsInactive.Exclude,
                FindObjectsSortMode.None
            );
            if (objs.Length > 1)
            {
                foreach (var obj in objs)
                {
                    var scope = (LifetimeScope) obj;
                    if (scope.gameObject.scene.name == "DontDestroyOnLoad"
                        && scope.Container != null)
                    {
                        return scope;
                    }
                }
            }
            else if (objs.Length > 0
                     && objs[0] is LifetimeScope scope
                     && scope.Container != null)
            {
                return scope;
            }
            return null;
        }
#endif
    }
}
#endif
