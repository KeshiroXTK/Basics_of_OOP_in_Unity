using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General.Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace General
{
    public class CameraController : ILateExecute
    {
        private Transform _player;
        private Transform _mainCamera;
        
        private Vector3 _offset;

        public CameraController(Transform player, Transform mainCamera)
        {
            _player = player;
            _mainCamera = mainCamera;
            _mainCamera.LookAt(_player);
            _offset = _mainCamera.localPosition - _player.position;
        }

        public void LateExecute(float deltaTime)
        {
            _mainCamera.localPosition = _player.position + _offset;
        }
    }
}