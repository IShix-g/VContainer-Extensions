#if ENABLE_VCONTAINER

namespace VContainer.Unity.Extensions
{
    public abstract class PersistentLifetimeScope<T> : PersistentLifetimeScopeBase where T : PersistentLifetimeScope<T>
    {
        static T s_instance;

        /// <summary>
        /// This block is executed only once and is executed before the initialization of the LifetimeScope.
        /// </summary>
        protected virtual void OnInitialize() {}
        /// <summary>
        /// This block is executed every time Awake() is called and runs before the initialization of the LifetimeScope.
        /// </summary>
        /// <param name="instance"></param>
        protected virtual void OnEveryAwake(T instance){}

        protected override void Awake()
        {
            if (s_instance == null)
            {
                s_instance = (T) this;
                DontDestroyOnLoad(gameObject);
                OnInitialize();
                OnEveryAwake(s_instance);
                base.Awake();
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
