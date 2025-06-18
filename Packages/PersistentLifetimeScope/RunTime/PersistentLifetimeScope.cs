#if ENABLE_VCONTAINER
using UnityEngine;

namespace VContainer.Unity.Extensions
{
    // Generic classes cannot be used in Unity Editor extensions, hence this is necessary
    public abstract class PersistentLifetimeScopeBase : LifetimeScope {}

    [HelpURL("https://github.com/IShix-g/VContainer-Extensions?tab=readme-ov-file#persistentlifetimescope")]
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
            Reset();
        }

        protected virtual void Reset()
        {
            parentReference = default;
            autoRun = true;
        }
    }
}
#endif
