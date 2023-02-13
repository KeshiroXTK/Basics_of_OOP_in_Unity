using System;
using System.Collections;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace General
{
    public class GameEnding
    {
        public Action RestartGame;
        
        private CanvasGroup _canvasGroup;
        private Button _restartButton;
        private Text _finishGameLabel;

        public GameEnding(GameObject endGame)
        {
            _finishGameLabel = endGame.GetComponentInChildren<Text>();
            _finishGameLabel.text = string.Empty;
            
            _restartButton = endGame.GetComponentInChildren<Button>();
            _restartButton.onClick.AddListener(OnRestartClick);
            
            _canvasGroup = endGame.GetComponentInChildren<CanvasGroup>();
            _canvasGroup.alpha = 0;
            
            _canvasGroup.gameObject.SetActive(false);
        }

        public void Display(string text)
        {
            _finishGameLabel.text = text;
            _canvasGroup.gameObject.SetActive(true);
            GameController.Instance.StartCoroutine(Show(1.5f));
        }
        
        private IEnumerator Show (float duration) {
            var startTime = Time.unscaledTime;
            var endTime = Time.unscaledTime + duration;

            while (Time.unscaledTime < endTime) {
                _canvasGroup.alpha = (Time.unscaledTime - startTime) / duration;;

                yield return null;
            }
        }

        private void OnRestartClick()
        {
            RestartGame?.Invoke();
        }
    }
}