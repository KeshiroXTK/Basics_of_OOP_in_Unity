using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General
{
    public class GoodBonus : InteractiveObject, IFlay, IRotation
    {
        [SerializeField]
        private int _points = 1;
        private float _lengthFlay;
        private float _speedRotation;
        
        public event Action<int> OnCollectPoint = delegate(int i) {  };
        
        private void Awake()
        {
            _lengthFlay = Random.Range(1.0f, 3.0f);
            _speedRotation = Random.Range(10.0f, 50.0f);
        }
        protected override void Interaction(GameObject player)
        {
            OnCollectPoint.Invoke(_points);
        }
        
        public override void PlayEffect(float deltaTime)
        {
            if(!IsInteractable){return;}
            Flay();
            Rotation(deltaTime);
        }

        public void Flay()
        {
            var localPosition = transform.localPosition;
            localPosition = new Vector3(localPosition.x, 
                Mathf.PingPong(Time.time, _lengthFlay),
                localPosition.z);
            transform.localPosition = localPosition;
        }

        public void Rotation(float deltaTime)
        {
            transform.Rotate(Vector3.up * (deltaTime * _speedRotation), Space.World);
        }
    }
}