using System;
using R3;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneStorage
    {
        public ReadOnlyReactiveProperty<int> Resources => _resources;
        public ReadOnlyReactiveProperty<int> ResourcesInDrone => _resourcesInDrone;
        public Observable<int> OnCollectDrone => _onCollectDrone;

        private readonly Subject<int> _onCollectDrone = new();
        private readonly ReactiveProperty<int> _resources = new(0);
        private readonly ReactiveProperty<int> _resourcesInDrone = new(0);

        public void AddResourceForDrone(int resourceAmount)
        {
            _resourcesInDrone.Value = Mathf.Min(
                GameContext.DroneUpgrades.StorageCapacity.CurrentValue, 
                _resourcesInDrone.Value + resourceAmount);
        }

        public void CollectDrone()
        {
            _onCollectDrone.OnNext(_resourcesInDrone.Value);
            _resources.Value += _resourcesInDrone.Value;
            _resourcesInDrone.Value = 0;
        }
    }
}