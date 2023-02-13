using System.Collections;
using System.Data;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace General
{
    public class SpeedBonus : InteractiveObject, IFlay
    {
        [SerializeField]
        private float _speedBonus = 1f;
        [SerializeField]
        private float _timeBonus = 1f;
        
        [SerializeField]
        private Vector3 _offcetPosition = Vector3.zero;
        
        private Vector3 _localPosition;

        private void Awake()
        {
            _localPosition = transform.localPosition;
        }
        
        protected override void Interaction(GameObject player)
        {
            if (_speedBonus == 0)
            {
                throw new DataException("bonus speed can't equal 0");
            } 
            if (_timeBonus <= 0)
            {
                throw new DataException("bonus time must be more than 0");
            }
            
            var playerScript = player.GetComponent<PlayerBase>();

            playerScript.AddSpeed(_speedBonus, _timeBonus);
        }
        
        public override void PlayEffect(float deltaTime)
        {
            if(!IsInteractable){return;}
            Flay();
        }

        public void Flay()
        {
            transform.position = Vector3.Lerp(_localPosition, _localPosition + _offcetPosition,
                Mathf.PingPong(Time.time, 1));
        }
    }
}