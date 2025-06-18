#if !ENABLE_VCONTAINER
using UnityEditor;
using UnityEngine;

namespace VContainer.Unity.Extensions.Editor
{
    [InitializeOnLoad]
    public sealed class VContainerChecker
    {
        static VContainerChecker()
        {
            var isInstall = EditorUtility.DisplayDialog(
                "VContainer Not Found",
                "VContainer is not installed in your project. Please install VContainer to use related features.",
                "Install",
                "Close"
            );

            if (isInstall)
            {
                Application.OpenURL("https://github.com/hadashiA/VContainer?tab=readme-ov-file#installation");
            }

            Debug.LogError("VContainer is not installed. Please install it to enable dependency injection.");
        }
    }
}
#endif
