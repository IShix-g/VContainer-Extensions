#if ENABLE_VCONTAINER
using UnityEngine;
using VContainer;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class UiInitializer : MonoBehaviour
    {
        [SerializeField] OpenAppTimeText _appTimeText;

        [Inject]
        public void Inject(OpenAppTimeTracker tracker)
        {
            _appTimeText.SetTime(tracker.SessionTotalDuration());
        }
    }
}
#endif
