#if ENABLE_VCONTAINER
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace VContainer.Unity.Extensions.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PersistentChildLifetimeScopeBase), true)]
    public sealed class PersistentChildLifetimeScopeEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            {
                var style = new GUIStyle(GUI.skin.box)
                {
                    alignment = TextAnchor.MiddleCenter,
                    padding = new RectOffset(0, 0, 5, 5),
                };
                GUILayout.Box("Child", style, GUILayout.ExpandWidth(true));
            }
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
                    var propertyName = property.name;
                    var isDisable = propertyName == "parent" || propertyName == "autoRun";
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
