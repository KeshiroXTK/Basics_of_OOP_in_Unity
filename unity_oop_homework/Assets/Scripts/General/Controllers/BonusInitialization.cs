using System.Collections.Generic;
using System.Linq;
using General.Interfaces;
using SaveData;
using UnityEngine;

namespace General.Controllers
{
    internal sealed class BonusInitialization : IInitialization
    {
        private CompositeEffect _effect;
        private List<InteractiveObject> _bonuses;
        
        public BonusInitialization(Dictionary<string, InteractiveObject> bonusesConfigs)
        {
            _bonuses = Object.FindObjectsOfType<InteractiveObject>().ToList();
            foreach (var bonus in _bonuses)
            {
                var pair = bonusesConfigs.FirstOrDefault(item => item.Value.Equals(bonus));
                if (!pair.Equals(default(KeyValuePair<string, InteractiveObject>)))
                {
                    bonus.Uid = pair.Key;
                }
            }
            _effect = new CompositeEffect(_bonuses);
            SaveDataRepository.Instance.Save(_bonuses);
        }

        public IEffect GetEffectBonuses()
        {
            return _effect;
        }

        public IEnumerable<InteractiveObject> GetBonuses()
        {
            return _bonuses;
        }

        public void Initialization()
        {

        }
    }
}