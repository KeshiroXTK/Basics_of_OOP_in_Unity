using General.Interfaces;
using UnityEngine;

namespace General.Controllers
{
    internal sealed class UiInitialization : IInitialization
    {
        private Canvas _canvas;

        public GameObject EndGame { get; private set; }
        public GameObject Score { get; private set; }
        public GameObject Radar { get; private set; }

        public UiInitialization(UiConfig config)
        {
            _canvas = Object.FindObjectOfType<Canvas>();
            Score = Object.Instantiate(config.score, _canvas.transform);
            EndGame = Object.Instantiate(config.endGame, _canvas.transform);
            Radar = Object.Instantiate(config.radarConfig.radar, _canvas.transform);
        }
        
        public void Initialization()
        {
            
        }
    }
}