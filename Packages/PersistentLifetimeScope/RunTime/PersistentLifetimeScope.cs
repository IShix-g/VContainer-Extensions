#if ENABLE_VCONTAINER

namespace VContainer.Unity.Extensions
{
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
