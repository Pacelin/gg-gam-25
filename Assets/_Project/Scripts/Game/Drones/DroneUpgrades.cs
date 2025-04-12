using System.Collections.Generic;
using R3;
using TSS.ContentManagement;

namespace GGJam25.Game.Drones
{
    public class DroneUpgrades
    {
        public ReactiveProperty<int> SpeedLevel => _speedLevel;
        public ReactiveProperty<int> StorageLevel => _storageLevel;
        public ReactiveProperty<int> VacuumLevel => _vacuumLevel;
        public ReactiveProperty<int> StabilizationLevel => _stabilizationLevel;

        public ReadOnlyReactiveProperty<float> LinearSpeed => 
            _speedLevel.Select(l => CMS.Upgrades.Speed.LinearSpeeds[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> AngularSpeed => 
            _speedLevel.Select(l => CMS.Upgrades.Speed.AngularSpeeds[l]).ToReadOnlyReactiveProperty();
        
        public ReadOnlyReactiveProperty<int> StorageCapacity => 
            _storageLevel.Select(l => CMS.Upgrades.Storage.Capacities[l]).ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<float> VacuumPower =>
            _vacuumLevel.Select(l => CMS.Upgrades.Vacuum.Power[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> VacuumWidth =>
            _vacuumLevel.Select(l => CMS.Upgrades.Vacuum.Width[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> VacuumDistance =>
            _vacuumLevel.Select(l => CMS.Upgrades.Vacuum.Distance[l]).ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<float> Stabilization =>
            _stabilizationLevel.Select(l => CMS.Upgrades.Stabilization.Stabilization[l]).ToReadOnlyReactiveProperty();

        private readonly ReactiveProperty<int> _speedLevel = new(0);
        private readonly ReactiveProperty<int> _storageLevel = new(0);
        private readonly ReactiveProperty<int> _vacuumLevel = new(0);
        private readonly ReactiveProperty<int> _stabilizationLevel = new(0);

        public IEnumerable<(ReactiveProperty<int> Level, DroneUpgrade Upgrade)> AllUpgrades()
        {
            yield return (_vacuumLevel, CMS.Upgrades.Vacuum);
            yield return (_storageLevel, CMS.Upgrades.Storage);
            yield return (_speedLevel, CMS.Upgrades.Speed);
            yield return (_stabilizationLevel, CMS.Upgrades.Stabilization);
        }
    }
}