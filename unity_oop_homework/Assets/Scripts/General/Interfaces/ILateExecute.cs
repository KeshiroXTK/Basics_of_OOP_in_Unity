﻿namespace General.Interfaces
{
    public interface ILateExecute : IController
    {
        void LateExecute(float deltaTime);
    }
}