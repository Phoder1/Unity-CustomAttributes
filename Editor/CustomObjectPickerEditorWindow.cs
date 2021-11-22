using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CustomAttributes
{
    public class CustomObjectPickerEditorWindow : EditorWindow
    {
        public static void GetWindow(System.Type type, CustomObjectPickerAttribute attr, System.Action<Object> callback)
        {
            GetWindow<CustomObjectPickerEditorWindow>(false, "Select Object", true);

            _callback = callback;
            FilterAssets(type, attr);
        }

        private static IEnumerable<Object> _allMatchingObjects;
        private static System.Action<Object> _callback;
        private Vector2 _scrollPos = Vector2.zero;


        protected void OnGUI()
        {
            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
            {
                if (GUILayout.Button("None"))
                    _callback.Invoke(null);

                foreach (var obj in _allMatchingObjects)
                {
                    EditorGUILayout.BeginHorizontal();

                    GUI.enabled = false;
                    EditorGUILayout.ObjectField(obj, typeof(Object), true);
                    GUI.enabled = true;

                    if (GUILayout.Button("Select", EditorStyles.miniButtonRight, GUILayout.Width(60)))
                    {
                        _callback.Invoke(obj);
                        Close();
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndScrollView();
        }


        private static void FilterAssets(System.Type type, CustomObjectPickerAttribute attr)
        {
            if (!typeof(Object).IsAssignableFrom(type))
            {
                _allMatchingObjects = Enumerable.Empty<Object>();
                throw new System.Exception("Field should be of a sub-type of UnityEngine.Object!");
            }

            _allMatchingObjects = Enumerable.Empty<Object>();
            IEnumerable<Object> foundObj = Enumerable.Empty<Object>();

            if (type.IsAssignableFrom(typeof(GameObject)) || typeof(GameObject).IsAssignableFrom(type))
            {
                foundObj = Resources.FindObjectsOfTypeAll(typeof(GameObject));
                if (attr.resultObjectType != ResultObjectType.SceneOrAsset)
                {
                    if (attr.resultObjectType == ResultObjectType.Scene)
                        foundObj = foundObj.Where((t) => t != null && !IsAPrefab<GameObject>(t));

                    if (attr.resultObjectType == ResultObjectType.Asset)
                        foundObj = foundObj.Where((t) => t != null && IsAPrefab<GameObject>(t));
                }

                // if we're dealing with GameObject references, then we'll restrict outrselves to any
                // GameObject with components attached that possess all type limitations collectively
                foreach (var restrictionType in attr.typeRestrictions)
                    foundObj = foundObj.Where(t => (t as GameObject).GetComponent(restrictionType) != null).ToList();

                _allMatchingObjects = _allMatchingObjects.Union(foundObj);
            }

            if (type.IsAssignableFrom(typeof(Component)) || typeof(Component).IsAssignableFrom(type))
            {
                foundObj = Resources.FindObjectsOfTypeAll(typeof(Component));
                if (attr.resultObjectType != ResultObjectType.SceneOrAsset)
                {
                    if (attr.resultObjectType == ResultObjectType.Scene)
                        foundObj = foundObj.Where(t => t != null && !IsAPrefab<Component>(t));

                    if (attr.resultObjectType == ResultObjectType.Asset)
                        foundObj = foundObj.Where(t => t != null && IsAPrefab<Component>(t));
                }

                // if we're dealing with components, then we limit ourselves to components that derive
                // or implement all restriction type
                foreach (var restrictionType in attr.typeRestrictions)
                    foundObj = foundObj.Where(t => restrictionType.IsAssignableFrom(t.GetType()));

                _allMatchingObjects = _allMatchingObjects.Union(foundObj);
            }

            if (type.IsAssignableFrom(typeof(ScriptableObject)) || typeof(ScriptableObject).IsAssignableFrom(type))
            {
                foundObj = Resources.FindObjectsOfTypeAll(typeof(ScriptableObject));
                // ScriptableObjects are assets only, so we can skip the asset/scene object check
                foreach (var restrictionType in attr.typeRestrictions)
                    foundObj = foundObj.Where(t => restrictionType.IsAssignableFrom(t.GetType()));

                _allMatchingObjects = _allMatchingObjects.Union(foundObj);
            }

            foundObj = null;
        }

        public static bool IsAPrefab<T>(Object obj) where T : Object
        {
            var prefabT = PrefabUtility.GetPrefabAssetType(obj as T);
            return prefabT == PrefabAssetType.Regular || prefabT == PrefabAssetType.Variant;
        }
    }
}