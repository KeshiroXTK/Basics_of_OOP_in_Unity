using System.Collections;
using UnityEditor;
using UnityEngine;

namespace Common.Editor
{
    public class MenuItems
    {
        [MenuItem("Assets/Find References In Project (ext.)", priority = 25)]
        private static void FindReferences()
        {
            var targetGuid = Selection.assetGUIDs[0];
            var progress = new CommonUtility.SearchProgress();
            var isCanseled = false;
            IEnumerator iterator;

            if (EditorSettings.serializationMode == SerializationMode.ForceText)
            {
                iterator = CommonUtility.FindReferencesByText(targetGuid, progress);
            }
            else
            {
                iterator = CommonUtility.FindReferences(targetGuid, progress);
            }

            EditorApplication.update += Proceed;

            void Proceed()
            {
                if (!isCanseled && iterator.MoveNext())
                {
                    isCanseled = EditorUtility.DisplayCancelableProgressBar("Searching references", "That could take a while...", progress.Progress);
                    return;
                }

                EditorApplication.update -= Proceed;

                EditorUtility.ClearProgressBar();

                if (progress.FoundObjects.Count == 0)
                {
                    Debug.Log("There are no dependencies.");
                    return;
                }

                ReferencesWindow.Create(targetGuid, progress.FoundObjects);
            }
        }
    }
}