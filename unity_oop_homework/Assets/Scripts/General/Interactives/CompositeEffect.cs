using System;
using System.Collections;
using System.Collections.Generic;
using General.Interfaces;

namespace General
{
    public class CompositeEffect : IEffect
    {
        private List<IEffect> _effects = new List<IEffect>();

        public CompositeEffect(IEnumerable<InteractiveObject> bonuses)
        {
            foreach (var item in bonuses)
            {
                if (item is IEffect bonus)
                {
                    AddUnit(bonus);
                }
            }
        }

        public void AddUnit(IEffect bonus)
        {
            _effects.Add(bonus);
        }

        public void RemoveUnit(IEffect bonus)
        {
            _effects.Remove(bonus);
        }
        
        public void PlayEffect(float deltaTime)
        {
            for (var i = 0; i < _effects.Count; i++)
            {
                _effects[i].PlayEffect(deltaTime);
            }
        }
    }
}