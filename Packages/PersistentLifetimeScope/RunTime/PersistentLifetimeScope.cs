#if ENABLE_VCONTAINER
using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VContainer.Unity.Extensions
{
    // Generic classes cannot be used in Unity Editor extensions, hence this is necessary
    public abstract class PersistentLifetimeScopeBase : LifetimeScope {}

    public abstract class PersistentLifetimeScope<T> : PersistentLifetimeScopeBase where T : PersistentLifetimeScope<T>
    {
        static T s_instance;

        protected override void Awake()
        {
            if (s_instance == null)
            {
                base.Awake();
                s_instance = (T) this;
                DontDestroyOnLoad(gameObject);
            }
            else if (s_instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected override void OnDestroy()
        {
            if (s_instance == this)
            {
                base.OnDestroy();
                s_instance = null;
            }
        }

        protected void OnValidate()
        {
            if (parentReference.Type != null)
            {
                Reset();
            }
        }

        protected virtual void Reset()
        {
            parentReference = default;
        }

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
            {
                if (objs.Length > 0
                    && objs[0] is LifetimeScope scope
                    && scope.Container != null)
                {
                    return scope;
                }
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
