using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneWater.asset", "Drone Water Upgrade")]
    public class DroneWaterUpgrade : DroneUpgrade
    {
        public float[] WaterProtection => _waterProtection;
        
        [Space] 
        [SerializeField] private string _safeValueStr = "Защита от влаги: ";
        [SerializeField] private float[] _waterProtection;

        protected override void OnView(int currentLevel, DroneUpgradeView view, bool isMax)
        {
            if (isMax)
                view.AddInfo(_safeValueStr, currentLevel.ToString());
            else
                view.AddInfo(_safeValueStr, 
                    currentLevel.ToString(), (currentLevel + 1).ToString());
        }
    }
}