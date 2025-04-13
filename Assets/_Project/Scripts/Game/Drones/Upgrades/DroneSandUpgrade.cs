using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneSand.asset", "Drone Sand Upgrade")]
    public class DroneSandUpgrade : DroneUpgrade
    {
        public float[] SandProtection => _sandProtection;
        
        [Space] 
        [SerializeField] private string _safeValueStr = "Защита от сухости: ";
        [SerializeField] private float[] _sandProtection;

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