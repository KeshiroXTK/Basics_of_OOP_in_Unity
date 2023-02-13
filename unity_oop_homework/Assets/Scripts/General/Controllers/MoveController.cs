using General.Interfaces;
using SaveData;
using UnityEngine;

namespace General.Controllers
{
    public class MoveController : IExecute, ICleanup
    {
        private readonly PlayerBase _playerBase;
        private float _horizontal;
        private float _vertical;
        private Vector3 _move;
        private IUserInputProxy _horizontalInputProxy;
        private IUserInputProxy _verticalInputProxy;
        
        public MoveController((IUserInputProxy inputHorizontal, IUserInputProxy inputVertical) input,
            PlayerBase player)
        {
            _playerBase = player;
            _horizontalInputProxy = input.inputHorizontal;
            _verticalInputProxy = input.inputVertical;
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;
        }
        
        private void VerticalOnAxisOnChange(float value)
        {
            _vertical = value;
        }

        private void HorizontalOnAxisOnChange(float value)
        {
            _horizontal = value;
        }

        public void Execute(float deltaTime)
        {
            _playerBase.Move(_horizontal, 0.0f, _vertical);

            if (Time.frameCount % 60 == 0)
            {
                SaveDataRepository.Instance.Save(_playerBase);
            }
        }

        public void Cleanup()
        {
            _horizontalInputProxy.AxisOnChange -= HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange -= VerticalOnAxisOnChange;
        }
    }
}