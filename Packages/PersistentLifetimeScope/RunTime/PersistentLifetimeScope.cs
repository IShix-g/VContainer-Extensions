#if ENABLE_VCONTAINER

namespace VContainer.Unity.Extensions
{
    public abstract class PersistentLifetimeScope<T> : PersistentLifetimeScopeBase where T : PersistentLifetimeScope<T>
    {
        static T s_instance;

        protected virtual void OnInitialize() {}
        protected virtual void OnEveryAwake(T instance){}

        protected override void Awake()
        {
            if (s_instance == null)
            {
                base.Awake();
                s_instance = (T) this;
                DontDestroyOnLoad(gameObject);
                OnInitialize();
                OnEveryAwake(s_instance);
            }
            else if (s_instance != this)
            {
                OnEveryAwake(s_instance);
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
