using System;

namespace SaveData.Models
{
    [Serializable]
    public sealed class Bonus
    {
        public string Uid;
        public Vector3Serializable Position;
        public bool IsEnabled;
    }
}