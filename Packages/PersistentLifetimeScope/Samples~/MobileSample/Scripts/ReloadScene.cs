
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PersistentLifetimeScope.MobileSample
{
    public sealed class ReloadScene : MonoBehaviour
    {
        static int s_instanceCount;

        void Start()
        {
            _ = Reload();
        }

        async Task Reload()
        {
            Debug.Log("Reload count = " + ++s_instanceCount);
            await Task.Delay(3000);
            var sceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(sceneName);
        }
    }
}
