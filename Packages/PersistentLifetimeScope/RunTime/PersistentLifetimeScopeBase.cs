#if ENABLE_VCONTAINER
using UnityEngine;

namespace VContainer.Unity.Extensions
{
    // Generic classes cannot be used in Unity Editor extensions, hence this is necessary
    [HelpURL("https://github.com/IShix-g/VContainer-Extensions?tab=readme-ov-file#persistentlifetimescope")]
    public abstract class PersistentLifetimeScopeBase : LifetimeScope {}
}
#endif
