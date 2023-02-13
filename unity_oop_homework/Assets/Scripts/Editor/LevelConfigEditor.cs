using System.Linq;
using General;
using General.Levels;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Configs.Editor
{
    [CustomEditor(typeof(LevelConfig), true)]
    public sealed class LevelConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            DrawImportButton();
        }
        
        private bool CanImport()
        {
            var stageHandle = StageUtility.GetCurrentStageHandle();
            if (!stageHandle.IsValid())
                return true;
            
            var location = stageHandle.FindComponentsOfType<BaseLevel>();

            return location == null || location.Length <= 0;
        }
        
        private void DrawImportButton()
        {
            EditorGUILayout.Space();

            var canImport = CanImport();
            GUI.enabled = canImport;
            if (!canImport)
                EditorGUILayout.HelpBox("Close level prefab before import!", MessageType.Warning);
            
            if (GUILayout.Button("Import..."))
            {
                foreach (var config in targets.Cast<LevelConfig>())
                {
                    config.ImportFromConfig();
                }
                EditorUtility.SetDirty(this);
            }

            GUI.enabled = true;
        }
    }
}