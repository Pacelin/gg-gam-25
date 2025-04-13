using TSS.Utils;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneRadar.asset", "Drone Radar Upgrade")]
    public class DroneRadarUpgrade : DroneUpgrade
    {
        protected override void OnView(int currentLevel, DroneUpgradeView view, bool isMax)
        {
        }
    }
}