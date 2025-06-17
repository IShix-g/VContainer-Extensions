
using UnityEngine;
using VContainer;

namespace Tests
{
    public sealed class TestChildObject : MonoBehaviour
    {
        [Inject]
        public void Inject(DisposableServiceB service)
        {
            Debug.Log("Injected : " + service);
        }
    }
}
