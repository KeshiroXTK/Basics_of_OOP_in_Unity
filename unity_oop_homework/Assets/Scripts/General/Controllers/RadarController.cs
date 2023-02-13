using System.Collections.Generic;
using General.Interfaces;
using UnityEngine;

namespace General.Controllers
{
    public sealed class Blip
    {
        public GoodBonus bonus;
        public RectTransform point;
    }
    
    public class RadarController : IInitialization, IExecute, ICleanup {
        private readonly float _insideRadarDistance;
        private readonly float _blipSizePercentage;
        private readonly RectTransform _radar;

        private GameObject _radarObject;
        private Transform _playerTransform;
        
        private float _radarWidth;
        private float _radarHeight;
        private float _blipHeight;
        private float _blipWidth;
    
        private List<Blip> _blips;

        public RadarController(RadarConfig radarConfig, GameObject radar, Transform player, IEnumerable<InteractiveObject> bonuses)
        {
            _playerTransform = player;
            _radar = radar.GetComponent<RectTransform>();
            _radarObject = radarConfig.radarObject;
            _insideRadarDistance = radarConfig.insideRadarDistance;
            _blipSizePercentage = radarConfig.blipSizePercentage;
            
            _blips = new List<Blip>();
            foreach (var bonus in bonuses)
            {
                if (!(bonus is GoodBonus goodBonus)) continue;
                var blip = new Blip
                {
                    bonus = goodBonus,
                    point = Object.Instantiate(_radarObject, _radar.GetChild(0)).GetComponent<RectTransform>()
                };

                _blips.Add(blip);
            }
        }
    
        public void Initialization()
        {
            var rect = _radar.rect;
            _radarWidth = rect.width;
            _radarHeight = rect.height;

            _blipHeight = _radarHeight * _blipSizePercentage/100;
            _blipWidth = _radarWidth * _blipSizePercentage/100;
            
        }

        public void Execute(float deltaTime)
        {
            if (Time.frameCount % 2 == 0)
            {
                DisplayBlips();
            }
        }

        private void DisplayBlips()
        {
            var playerPos = _playerTransform.position;

            foreach (var blip in _blips) {
                if (!blip.bonus.IsInteractable)
                {
                    blip.point.gameObject.SetActive(false);
                    continue;
                }
                
                var targetPos = blip.bonus.transform.position;
                var normalisedTargetPosiiton = NormalizedPosition(playerPos, targetPos);
                var blipPosition = CalculateBlipPosition(normalisedTargetPosiiton);
                DrawBlip(blipPosition, blip);
            }
        }

        private void RemoveAllBlips()
        {
            foreach (var blip in _blips)
            {
                Object.Destroy(blip.point.gameObject);
            }
        }

        private Vector3 NormalizedPosition(Vector3 playerPos, Vector3 targetPos)
        {
            var normalizedTargetX = (targetPos.x - playerPos.x) / _insideRadarDistance;
            var normalizedTargetZ = (targetPos.z - playerPos.z) / _insideRadarDistance;
            return new Vector3(normalizedTargetX, 0, normalizedTargetZ);
        }

        private Vector2 CalculateBlipPosition(Vector3 targetPos)
        {
            // find angle from player to target
            float angleToTarget = Mathf.Atan2(targetPos.x, targetPos.z) * Mathf.Rad2Deg;

            // direction player facing
            float anglePlayer = _playerTransform.eulerAngles.y;

            // subtract player angle, to get relative angle to object
            // subtract 90
            // (so 0 degrees (same direction as player) is UP)
            float angleRadarDegrees = angleToTarget /*- anglePlayer*/ - 90;

            // calculate (x,y) position given angle and distance
            float normalisedDistanceToTarget = targetPos.magnitude;
            float angleRadians = angleRadarDegrees * Mathf.Deg2Rad;
            float blipX = normalisedDistanceToTarget * Mathf.Cos(angleRadians);
            float blipY = normalisedDistanceToTarget * Mathf.Sin(angleRadians);

            // scale blip position according to radar size
            blipX *= _radarWidth/2;
            blipY *= _radarHeight/2;

            // offset blip position relative to radar center
            blipX += _radarWidth/2;
            blipY += _radarHeight/2;

            return new Vector2(blipX, blipY);
        }

        private void DrawBlip(Vector2 pos, Blip blip)
        {
            blip.point.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, pos.x, _blipWidth);
            blip.point.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, pos.y, _blipHeight);
        }

        public void Cleanup()
        {
            RemoveAllBlips();
        }
    }
}