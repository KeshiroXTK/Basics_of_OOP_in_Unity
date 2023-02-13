using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig", order = 0)]
    public sealed partial class LevelConfig : ScriptableObject
    {
        public int totalPoints;
        public GameObject levelPrefab;
    }
}