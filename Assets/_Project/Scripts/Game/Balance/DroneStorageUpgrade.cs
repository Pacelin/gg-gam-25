using R3;
using TSS.ContentManagement;
using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Balance
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
            base.OnView(currentLevel, view);
            if (isMax)
                view.AddInfo(_capacityValueStr, _capacityValueStr[currentLevel].ToString());
            else
                view.AddInfo(_capacityValueStr, 
                    _capacities[currentLevel].ToString(), _capacities[currentLevel + 1].ToString());
        }
    }

    public class DroneUpgrades
    {
        public ReactiveProperty<int> SpeedLevel => _speedLevel;
        public ReactiveProperty<int> StorageLevel => _storageLevel;

        public ReadOnlyReactiveProperty<float> DroneLinearSpeed => 
            _speedLevel.Select(l => CMS.Upgrades.Speed.LinearSpeeds[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> DroneAngularSpeed => 
            _speedLevel.Select(l => CMS.Upgrades.Speed.AngularSpeeds[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<int> DroneStorageCapacity => 
            _storageLevel.Select(l => CMS.Upgrades.Storage.Capacities[l]).ToReadOnlyReactiveProperty();

        private readonly ReactiveProperty<int> _speedLevel = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _storageLevel = new ReactiveProperty<int>(0);
    }
}