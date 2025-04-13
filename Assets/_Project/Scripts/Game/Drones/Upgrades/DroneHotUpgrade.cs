using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneHot.asset", "Drone Hot Upgrade")]
    public class DroneHotUpgrade : DroneUpgrade
    {
        public float[] HotProtection => _hotProtection;
        
        [Space] 
        [SerializeField] private string _safeValueStr = "Защита от тепла: ";
        [SerializeField] private float[] _hotProtection;

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