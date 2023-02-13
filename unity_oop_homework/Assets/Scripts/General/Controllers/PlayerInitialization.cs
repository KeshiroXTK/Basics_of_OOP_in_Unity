using General.Interfaces;
using UnityEngine;

namespace General.Controllers
{
    internal sealed class PlayerInitialization : IInitialization
    {
        private PlayerBase _player;

        public PlayerInitialization(PlayerBase player)
        {
            _player = Object.Instantiate(player);
        }
        
        public PlayerBase GetPlayer(PlayerType type)
        {
            return _player;
        }

        public void Initialization()
        {

        }
    }
}