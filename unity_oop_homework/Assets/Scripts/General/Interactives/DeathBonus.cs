using System;
using UnityEngine;

namespace General
{
    public class DeathBonus : InteractiveObject, IFlay
    {
        [SerializeField]
        private Vector3 _offcetPosition = Vector3.zero;
        
        public event Action OnDeath;
        
        private Vector3 _localPosition;

        private void Awake()
        {
            _localPosition = transform.localPosition;
        }
        protected override void Interaction(GameObject player)
        {
            //Destroy(player);
            OnDeath?.Invoke();
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