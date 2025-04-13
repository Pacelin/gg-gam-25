using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneCold.asset", "Drone Cold Upgrade")]
    public class DroneColdUpgrade : DroneUpgrade
    {
        public float[] ColdProtection => _coldProtection;
        
        [Space] 
        [SerializeField] private string _safeValueStr = "Защита от холода: ";
        [SerializeField] private float[] _coldProtection;

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