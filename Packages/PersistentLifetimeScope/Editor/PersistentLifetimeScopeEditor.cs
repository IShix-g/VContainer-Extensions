#if ENABLE_VCONTAINER
using System.Linq;
using System.Reflection;
using UnityEditor;
using VContainer.Unity.Extensions;

namespace VContainer.Editor.Extensions
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PersistentLifetimeScopeBase), true)]
    public sealed class PersistentLifetimeScopeEditor : UnityEditor.Editor
    {
        static string[] s_lifetimeScopeFields;
        
        void OnEnable()
        {
            if (s_lifetimeScopeFields != null)
            {
                return;
            }
            s_lifetimeScopeFields = typeof(global::VContainer.Unity.LifetimeScope)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Select(field => field.Name)
                .ToArray();
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                var property = serializedObject.FindProperty("m_Script");
                if (property != null)
                {
                    EditorGUI.BeginDisabledGroup(true);
                    EditorGUILayout.PropertyField(property, true);
                    EditorGUI.EndDisabledGroup();
                }
            }
            {
                var property = serializedObject.GetIterator();
                property.NextVisible(true);
                while (property.NextVisible(false))
                {
                    var isDisable = s_lifetimeScopeFields.Contains(property.name);
                    EditorGUI.BeginDisabledGroup(isDisable);
                    EditorGUILayout.PropertyField(property, true);
                    EditorGUI.EndDisabledGroup();
                }
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif