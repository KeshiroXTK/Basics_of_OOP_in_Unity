using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General
{
    public class CameraShakeController : IInitialization, ICleanup
    {
        private Transform _mainCamera;
        private Transform _cameraHolder;
        private Vector3 _originalPosition;

        private List<GoodBonus> _bonuses;

        public CameraShakeController(Transform mainCamera, IEnumerable<InteractiveObject> bonuses)
        {
            _mainCamera = mainCamera;
            _cameraHolder = new GameObject("cameraHolder").transform;
            _mainCamera.parent = _cameraHolder;
            
            _bonuses = new List<GoodBonus>();

            foreach (var bonus in bonuses)
            {
                if (bonus is GoodBonus goodBonus)
                {
                    _bonuses.Add(goodBonus);
                }
            }
            //_bonuses = (IEnumerable<GoodBonus>) bonuses.Where(bonus => bonus is GoodBonus);
        }
        
        public void Initialization()
        {
            foreach (var bonus in _bonuses)
            {
                bonus.OnCollectPoint += OnCollectPoint;
            }
        }

        private void OnCollectPoint(int points)
        {
            Shake(.5f,.5f);
        }

        private void Shake (float duration, float amount)
        {
            _originalPosition = _cameraHolder.position;
            GameController.Instance.StopCoroutine(cShake(duration, amount));
            GameController.Instance.StartCoroutine(cShake(duration, amount));
        }

        private IEnumerator cShake (float duration, float amount) {
            float endTime = Time.time + duration;

            while (Time.time < endTime) {
                _cameraHolder.position += Random.insideUnitSphere * amount;
                
                duration -= Time.deltaTime;

                yield return null;
            }

            _cameraHolder.position = _originalPosition;
        }
        
        public void Cleanup()
        {
            foreach (var bonus in _bonuses)
            {
                bonus.OnCollectPoint -= OnCollectPoint;
            }
        }
    }
}