using System.Collections.Generic;
using General.Interfaces;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace General.Controllers
{
    internal sealed class UiController : IInitialization, ICleanup
    {
        private IEnumerable<InteractiveObject> _bonuses;
        private DisplayBonuses _displayBonuses;
        private GameEnding _gameEnding;
        private int _totalPoints;
        private int _points = 0;

        public UiController(UiInitialization uiInitialization, IEnumerable<InteractiveObject> bonuses, int totalPoints)
        {
            _totalPoints = totalPoints;
            _bonuses = bonuses;
            _displayBonuses = new DisplayBonuses(uiInitialization.Score, totalPoints);
            
            _gameEnding = new GameEnding(uiInitialization.EndGame)
            {
                RestartGame = RestartGame
            };
        }

        public void Initialization()
        {
            foreach (var bonus in _bonuses)
            {
                if (bonus is GoodBonus goodBonus)
                {
                    goodBonus.OnCollectPoint += OnCollectPoint;
                } else if (bonus is DeathBonus deathBonus)
                {
                    deathBonus.OnDeath += OnDeathPlayer;
                }
            }
        }

        public void Cleanup()
        {
            
            foreach (var bonus in _bonuses)
            {
                if (bonus is GoodBonus goodBonus)
                {
                    goodBonus.OnCollectPoint -= OnCollectPoint;
                } else if (bonus is DeathBonus deathBonus)
                {
                    deathBonus.OnDeath -= OnDeathPlayer;
                }
            }
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(0);
            Time.timeScale = 1.0f;
        }
        
        private void OnDeathPlayer()
        {
            Time.timeScale = 0.0f;
            _gameEnding.Display("Game Over");
        }
        
        private void OnCollectPoint(int points)
        {
            _points += points;
            _displayBonuses.Display(_points);

            if (_points == _totalPoints)
            {
                _gameEnding.Display("You Win");
            }
        }
    }
}