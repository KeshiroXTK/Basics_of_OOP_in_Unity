using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace Common.Editor
{
    public static class CommonUtility
    {
        public class SearchProgress
        {
            public List<object> FoundObjects = new List<object>();
            public float Progress;
        }

        public static UnityObject LoadAssetByGuid(string guid)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            return AssetDatabase.LoadAssetAtPath(path, typeof(UnityObject));
        }

        public static IEnumerator FindReferences(string targetGuid, SearchProgress progress)
        {
            var assetGuids = AssetDatabase.FindAssets(string.Empty);
            var count = assetGuids.Length;
            var actionsPerFrame = count / 2;
            
            yield return null;

            for (var i = 0; i < count; i++)
            {
                progress.Progress = (i + 1f) / count;

                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuids[i]);
                var dependencies = AssetDatabase.GetDependencies(assetPath);

                foreach (var dependency in dependencies)
                {
                    var dependencyGuid = AssetDatabase.AssetPathToGUID(dependency);

                    if (dependencyGuid == targetGuid && dependencyGuid != assetGuids[i])
                    {
                        progress.FoundObjects.Add(assetGuids[i]);
                    }
                }

                if (i % actionsPerFrame == 0)
                {
                    yield return null;
                }
            }
        }

        public static IEnumerator FindReferencesByText(string targetGuid, SearchProgress progress)
        {
            var assetsFolderPath = Application.dataPath;
            var files = Directory.GetFiles(assetsFolderPath, "*", SearchOption.AllDirectories);

            yield return null;

            var projectFolderPath = Path.GetFullPath(Path.Combine( assetsFolderPath, ".." ));
            var count = files.Length;
            var actionsPerFrame = count / 2;
            
            yield return null;

            for (var i = 0; i < count; i++)
            {
                progress.Progress = (i + 1f) / count;

                var filePath = files[i];
                var extension = Path.GetExtension(filePath);

                var invalid = extension != ".prefab" &&
                              extension != ".unity" &&
                              extension != ".asset" &&
                              extension != ".mat" &&
                              extension != ".preset" &&
                              extension != ".anim" &&
                              extension != ".controller" &&
                              extension != ".spriteatlas" &&
                              !extension.Contains("override");

                if (invalid)
                {
                    continue;
                }

                var text = File.ReadAllText(filePath);

                if (text.Contains(targetGuid))
                {
                    var assetPath = filePath.Remove(0, projectFolderPath.Length + 1);
                    var guid = AssetDatabase.AssetPathToGUID(assetPath);
                    progress.FoundObjects.Add(guid);
                }

                if (i % actionsPerFrame == 0)
                {
                    yield return null;
                }
            }
        }
    }
}
