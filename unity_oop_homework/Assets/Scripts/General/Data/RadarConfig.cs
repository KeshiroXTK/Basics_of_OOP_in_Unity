using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "RadarConfig", menuName = "Configs/RadarConfig", order = 0)]
    public class RadarConfig : ScriptableObject
    {
        public float insideRadarDistance = 20;
        public float blipSizePercentage = 5;
        public GameObject radar;
        public GameObject radarObject;
    }
}