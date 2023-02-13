using System;
using System.Collections.Generic;
using UnityEngine;

namespace General
{
    [Serializable]
    public struct BonusConfig {
        public string name;
        public InteractiveObject prefab;
    }
    
    
    [CreateAssetMenu(fileName = "BonusesConfig", menuName = "Configs/BonusesConfig", order = 0)]
    public class BonusesConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<BonusConfig> _bonusConfigs;
        
        private Dictionary<string, InteractiveObject> _bonusesConfigs;

        public Dictionary<string, InteractiveObject> BonusesConfigs => _bonusesConfigs;

        public void OnBeforeSerialize() {}

        public void OnAfterDeserialize()
        {
            _bonusesConfigs = new Dictionary<string, InteractiveObject>();

            foreach (var bonus in _bonusConfigs)
            {
                _bonusesConfigs.Add(bonus.name, bonus.prefab);
            }

        }
        
        
    }
}