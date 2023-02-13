using UnityObject = UnityEngine.Object;

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Common.Editor
{
    internal class ReferencesWindow : EditorWindow
    {
        private UnityObject _target;
        private UnityObject[] _objects;
        private Vector2 _scrollPosition;

        public static void Create(string targetObjectGuid, List<object> referingObjectGuids)
        {
            var window = GetWindow<ReferencesWindow>("References");

            window._target = CommonUtility.LoadAssetByGuid(targetObjectGuid);
            window._objects = referingObjectGuids.Select(item => CommonUtility.LoadAssetByGuid(item.ToString())).ToArray();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space(5f);

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("References for", GUILayout.MaxWidth(100f));

            GUI.enabled = false;

            EditorGUILayout.ObjectField(_target, typeof(UnityObject), false);

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(5f);

            GUI.enabled = true;

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            GUI.enabled = false;

            for (var i = 0; i < _objects.Length; i++)
            {
                EditorGUILayout.ObjectField(_objects[i], typeof(UnityObject), false);
            }

            GUI.enabled = true;

            EditorGUILayout.EndScrollView();
        }
    }
}
