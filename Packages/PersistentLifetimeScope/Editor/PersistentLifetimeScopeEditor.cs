#if ENABLE_VCONTAINER
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using VContainer.Unity.Extensions;

namespace VContainer.Editor.Extensions
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PersistentLifetimeScopeBase), true)]
    public sealed class PersistentLifetimeScopeEditor : UnityEditor.Editor
    {
        static string[] s_lifetimeScopeFields;
        GameObject _targetObject;
        int _targetObjectChildCount = -1;

        void OnEnable()
        {
            _targetObject = ((Component) serializedObject.targetObject).gameObject;

            if (s_lifetimeScopeFields != null)
            {
                return;
            }
            s_lifetimeScopeFields = typeof(global::VContainer.Unity.LifetimeScope)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Select(field => field.Name)
                .Where(name => !name.Contains("autoInjectGameObjects"))
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
                    var propertyName = property.name;
                    var isDisable = s_lifetimeScopeFields.Contains(propertyName);
                    var objectsLength = propertyName == "autoInjectGameObjects"
                                            ? property.arraySize
                                            : -1;
                    EditorGUI.BeginDisabledGroup(isDisable);
                    EditorGUI.BeginChangeCheck();
                    EditorGUILayout.PropertyField(property, true);
                    var isChanged = EditorGUI.EndChangeCheck();
                    EditorGUI.EndDisabledGroup();

                    if (objectsLength < 0)
                    {
                        continue;
                    }

                    var currentChildCount = _targetObject.transform.childCount;
                    if ((property.arraySize <= 0
                         || objectsLength == property.arraySize)
                         && !isChanged
                         && currentChildCount == _targetObjectChildCount)
                    {
                        continue;
                    }

                    _targetObjectChildCount = currentChildCount;
                    ValidObjects(property);
                }
            }
            serializedObject.ApplyModifiedProperties();
        }

        void ValidObjects(SerializedProperty property)
        {
            for (var i = 0; i < property.arraySize; i++)
            {
                var element = property.GetArrayElementAtIndex(i);
                if (element.objectReferenceValue is not GameObject obj)
                {
                    continue;
                }

                if (obj != _targetObject
                    && (obj.transform.parent == null
                        || obj.transform.parent.gameObject != _targetObject))
                {
                    property.DeleteArrayElementAtIndex(i);
                    Debug.LogError(obj.name + " is not a child of " + _targetObject.name + ". Please make sure to specify either the object itself or one of its children.", obj);
                }
            }
        }
    }
}
#endif
