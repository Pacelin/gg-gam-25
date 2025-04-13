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
        public ReactiveProperty<int> RadarLevel => _radarLevel;
        public ReactiveProperty<int> HotUpgradeLevel => _hotUpgradeLevel;
        public ReactiveProperty<int> ColdUpgradeLevel => _coldUpgradeLevel;
        public ReactiveProperty<int> WaterUpgradeLevel => _waterUpgradeLevel;
        public ReactiveProperty<int> SandUpgradeLevel => _sandUpgradeLevel;

        public ReadOnlyReactiveProperty<float> LinearSpeed => 
            _speedLevel.Select(l => CMS.Upgrades.Speed.LinearSpeeds[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> AngularSpeed => 
            _speedLevel.Select(l => CMS.Upgrades.Speed.AngularSpeeds[l]).ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<float> HotProtection =>
            _hotUpgradeLevel.Select(l => CMS.Upgrades.Hot.HotProtection[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> ColdProtection =>
            _coldUpgradeLevel.Select(l => CMS.Upgrades.Cold.ColdProtection[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> WaterProtection =>
            _waterUpgradeLevel.Select(l => CMS.Upgrades.Water.WaterProtection[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> SandProtection =>
            _sandUpgradeLevel.Select(l => CMS.Upgrades.Sand.SandProtection[l]).ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<int> StorageCapacity => 
            _storageLevel.Select(l => CMS.Upgrades.Storage.Capacities[l]).ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<float> VacuumPower =>
            _vacuumLevel.Select(l => CMS.Upgrades.Vacuum.Power[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> VacuumAngle =>
            _vacuumLevel.Select(l => CMS.Upgrades.Vacuum.Width[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<float> VacuumDistance =>
            _vacuumLevel.Select(l => CMS.Upgrades.Vacuum.Distance[l]).ToReadOnlyReactiveProperty();

        public ReadOnlyReactiveProperty<float> Stabilization =>
            _stabilizationLevel.Select(l => CMS.Upgrades.Stabilization.Stabilization[l]).ToReadOnlyReactiveProperty();
        public ReadOnlyReactiveProperty<bool> RadarEnabled =>
            _radarLevel.Select(l => l >= 1).ToReadOnlyReactiveProperty();

        private readonly ReactiveProperty<int> _speedLevel = new(0);
        private readonly ReactiveProperty<int> _storageLevel = new(0);
        private readonly ReactiveProperty<int> _vacuumLevel = new(0);
        private readonly ReactiveProperty<int> _stabilizationLevel = new(0);
        private readonly ReactiveProperty<int> _radarLevel = new(0);

        private readonly ReactiveProperty<int> _hotUpgradeLevel = new(0);
        private readonly ReactiveProperty<int> _coldUpgradeLevel = new(0);
        private readonly ReactiveProperty<int> _waterUpgradeLevel = new(0);
        private readonly ReactiveProperty<int> _sandUpgradeLevel = new(0);

        public IEnumerable<(ReactiveProperty<int> Level, DroneUpgrade Upgrade)> AllUpgrades()
        {
            yield return (_radarLevel, CMS.Upgrades.Radar);
            yield return (_vacuumLevel, CMS.Upgrades.Vacuum);
            yield return (_storageLevel, CMS.Upgrades.Storage);
            yield return (_speedLevel, CMS.Upgrades.Speed);
            yield return (_stabilizationLevel, CMS.Upgrades.Stabilization);
            yield return (_hotUpgradeLevel, CMS.Upgrades.Hot);
            yield return (_coldUpgradeLevel, CMS.Upgrades.Cold);
            yield return (_waterUpgradeLevel, CMS.Upgrades.Water);
            yield return (_sandUpgradeLevel, CMS.Upgrades.Sand);
        }
    }
}