using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneStorage.asset", "Drone Storage Upgrade")]
    public class DroneStorageUpgrade : DroneUpgrade
    {
        public int[] Capacities => _capacities;
        
        [Space] 
        [SerializeField] private string _capacityValueStr = "Вместимость склада: ";
        [SerializeField] private int[] _capacities;

        protected override void OnView(int currentLevel, DroneUpgradeView view, bool isMax)
        {
            if (isMax)
                view.AddInfo(_capacityValueStr, _capacityValueStr[currentLevel].ToString());
            else
                view.AddInfo(_capacityValueStr, 
                    _capacities[currentLevel].ToString(), _capacities[currentLevel + 1].ToString());
        }
    }
}