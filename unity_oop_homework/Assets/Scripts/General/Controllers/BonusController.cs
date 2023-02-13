using System.Collections;
using General.Interfaces;

namespace General.Controllers
{
    public class BonusController : IExecute
    {
        private IEffect _effect;
        
        public BonusController(IEffect effect)
        {
            _effect = effect;
        }

        public void Execute(float deltaTime)
        {
            _effect.PlayEffect(deltaTime);
        }
    }
}