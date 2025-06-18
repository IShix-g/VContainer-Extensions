#if ENABLE_VCONTAINER
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VContainer.Unity.Extensions
{
    [HelpURL("https://github.com/IShix-g/VContainer-Extensions?tab=readme-ov-file#persistentlifetimescope")]
    public abstract class PersistentChildLifetimeScope : LifetimeScope
    {
        protected override LifetimeScope FindParent()
        {
            if (parentReference.Type == null)
            {
                return null;
            }

            var objs = FindObjects(parentReference.Type);
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

        static Object[] FindObjects(Type type)
        {
#if UNITY_2022_1_OR_NEWER
            return FindObjectsByType(type, FindObjectsInactive.Exclude, FindObjectsSortMode.None);
#else
            return FindObjectsOfType(type);
#endif
        }
    }
}
#endif
