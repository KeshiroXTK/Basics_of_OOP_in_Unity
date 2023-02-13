#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using SaveData.Models;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace General
{
    public partial class LevelConfig
    {
        [SerializeField]
        private BonusesConfig _bonusesConfig;
        
        public void ImportFromConfig()
        {
            var mapFilePath = EditorUtility.OpenFilePanel("Select location JSON file...", "", "json");
            if (mapFilePath == "")
                return;
            
            var mapTextData = File.ReadAllText(mapFilePath);
            var map = JsonUtility.FromJson<LevelData>(mapTextData);

            var progress = 0.0f;

            var assetPath = AssetDatabase.GetAssetPath(levelPrefab);
            var prefabRoot = PrefabUtility.LoadPrefabContents(assetPath);
            
            foreach (var bonus in map.bonuses)
            {
                var bonusPrefab = _bonusesConfig.BonusesConfigs[bonus.Uid];

                var loadedBonus = (InteractiveObject) PrefabUtility.InstantiatePrefab(bonusPrefab, prefabRoot.transform);
                loadedBonus.transform.position = bonus.Position;

                if (EditorUtility.DisplayCancelableProgressBar("Map parsing ...",
                    $"{(bonus.Uid ?? "unknown")}",
                    progress))
                    throw new Exception("Import canceled by you!");
                progress += 1.0f / map.bonuses.Count;
            }

            PrefabUtility.SaveAsPrefabAsset(prefabRoot, assetPath);
            PrefabUtility.UnloadPrefabContents(prefabRoot);
        }
    }

    public class LevelData
    {
        public List<Bonus> bonuses;
    }
}
#endif