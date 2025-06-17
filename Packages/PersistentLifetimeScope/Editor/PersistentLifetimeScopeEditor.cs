#if ENABLE_VCONTAINER
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace VContainer.Unity.Extensions.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PersistentLifetimeScopeBase), true)]
    public sealed class PersistentLifetimeScopeEditor : UnityEditor.Editor
    {
        static readonly string s_autoInjectFieldName = "autoInjectGameObjects";
        static string[] s_lifetimeScopeFields;
        GameObject _object;
        int _objectChildCount = -1;

        void OnEnable()
        {
            _object = ((Component) serializedObject.targetObject).gameObject;

            if (s_lifetimeScopeFields != null)
            {
                return;
            }
            s_lifetimeScopeFields = typeof(global::VContainer.Unity.LifetimeScope)
                .GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Select(field => field.Name)
                .Where(name => !name.Contains(s_autoInjectFieldName))
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
                    var objectsLength = propertyName == s_autoInjectFieldName
                                            ? property.arraySize
                                            : -1;
                    EditorGUI.BeginDisabledGroup(isDisable);
                    if (objectsLength >= 0)
                    {
                        EditorGUI.BeginChangeCheck();
                    }
                    EditorGUILayout.PropertyField(property, true);
                    var isChanged = objectsLength >= 0 && EditorGUI.EndChangeCheck();
                    EditorGUI.EndDisabledGroup();

                    if (objectsLength < 0)
                    {
                        continue;
                    }

                    var childCount = _object.transform.childCount;
                    if ((property.arraySize <= 0
                         || objectsLength == property.arraySize)
                         && !isChanged
                         && childCount == _objectChildCount)
                    {
                        continue;
                    }

                    _objectChildCount = childCount;
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

                if (obj != _object
                    && (obj.transform.parent == null
                        || obj.transform.parent.gameObject != _object))
                {
                    property.DeleteArrayElementAtIndex(i);
                    Debug.LogError(obj.name + " is not a child of " + _object.name + ". Please make sure to specify either the object itself or one of its children.", obj);
                }
            }
        }
    }
}
#endif
