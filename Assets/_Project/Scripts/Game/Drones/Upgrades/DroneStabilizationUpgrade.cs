using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneStabilization.asset", "Drone Stabilization Upgrade")]
    public class DroneStabilizationUpgrade : DroneUpgrade
    {
        public float[] Stabilization => _stabilization;
        
        [Space] 
        [SerializeField] private string _safeValueStr = "Стабильность: ";
        [SerializeField] private float[] _stabilization;

        protected override void OnView(int currentLevel, DroneUpgradeView view, bool isMax)
        {
            base.OnView(currentLevel, view);
            if (isMax)
                view.AddInfo(_safeValueStr, currentLevel.ToString());
            else
                view.AddInfo(_safeValueStr, 
                    currentLevel.ToString(), (currentLevel + 1).ToString());
        }
    }
}