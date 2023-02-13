using System.Collections.Generic;
using UnityEngine;

namespace General.Controllers
{
    internal sealed class GameInitialization
    {
        public IEnumerable<InteractiveObject> Bonuses { get; private set; }
        
        public GameInitialization(ControllersHandler controllersHandler, GameConfig data)
        {
            Camera camera = Camera.main;

            var levelInitialization = new LevelInitialization(data.levelConfig);

            var bonusInitialization = new BonusInitialization(data.bonusesConfig.BonusesConfigs);
            Bonuses = bonusInitialization.GetBonuses();

            foreach (var bonus in Bonuses)
            {
                controllersHandler.Add(bonus);
            }
            
            var inputInitialization = new InputInitialization();
            
            var playerInitialization = new PlayerInitialization(data.playerPrefab);

            var player = playerInitialization.GetPlayer(PlayerType.Ball);
            
            var uiInitialization = new UiInitialization(data.uiConfig);

            controllersHandler.Add(levelInitialization);
            controllersHandler.Add(bonusInitialization);
            controllersHandler.Add(inputInitialization);
            controllersHandler.Add(playerInitialization);
            controllersHandler.Add(uiInitialization);
            controllersHandler.Add(new InputController(inputInitialization.GetInput()));
            controllersHandler.Add(new MoveController(inputInitialization.GetInput(), player));
            controllersHandler.Add(new BonusController(bonusInitialization.GetEffectBonuses()));
            controllersHandler.Add(new CameraController(player.transform, camera.transform));
            controllersHandler.Add(new CameraShakeController(camera.transform, Bonuses));
            controllersHandler.Add(new RadarController(data.uiConfig.radarConfig, uiInitialization.Radar, player.transform, Bonuses));
            controllersHandler.Add(new UiController(uiInitialization, Bonuses, data.levelConfig.totalPoints));
        }

    }
}