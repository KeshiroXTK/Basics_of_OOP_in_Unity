using System;
using General.Interfaces;
using SaveData.Models;
using UnityEngine;

namespace General
{
    public abstract class InteractiveObject : MonoBehaviour, IInitialization, IEffect, IEquatable<InteractiveObject>
    {
        private bool _isInteractable = true;

        public string Uid;

        public bool IsInteractable
        {
            get { return _isInteractable; }
            private set
            {
                _isInteractable = value;
                gameObject.SetActive(_isInteractable);
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!IsInteractable || !other.CompareTag("Player"))
            {
                return;
            }
            Interaction(other.gameObject);
            IsInteractable = false;
        }

        protected abstract void Interaction(GameObject player);
        public abstract void PlayEffect(float deltaTime);

        public void Initialization()
        {
            IsInteractable = true;
        }

        public bool Equals(InteractiveObject other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GetType() == other.GetType();
        }

        public bool Equals(Bonus other)
        {
            if (ReferenceEquals(null, other)) return false;
            return GetType() == other.GetType() && transform.localPosition.Equals(other.Position);
        }
    }
}