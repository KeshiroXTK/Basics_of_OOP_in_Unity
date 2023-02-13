using System;
using System.Collections.Generic;
using SaveData.Models;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public sealed class SavedData
    {
        public List<Bonus> bonuses;
        public Player player;

        public SavedData()
        {
            bonuses = new List<Bonus>();
        }
    }
    
    [Serializable]
    public struct Vector3Serializable
    {
        public float X;
        public float Y;
        public float Z;

        private Vector3Serializable(float valueX, float valueY, float valueZ)
        {
            X = valueX;
            Y = valueY;
            Z = valueZ;
        }

        public static implicit operator Vector3(Vector3Serializable value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }
		
        public static implicit operator Vector3Serializable(Vector3 value)
        {
            return new Vector3Serializable(value.x, value.y, value.z);
        }
        
        public override string ToString() => $" (X = {X} Y = {Y} Z = {Z})";
    }
}