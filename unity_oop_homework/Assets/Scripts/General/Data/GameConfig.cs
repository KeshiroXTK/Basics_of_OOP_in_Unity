using UnityEngine;

namespace General
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public PlayerBall playerPrefab;
        public LevelConfig levelConfig;
        public BonusesConfig bonusesConfig;
        public UiConfig uiConfig;
    }
}