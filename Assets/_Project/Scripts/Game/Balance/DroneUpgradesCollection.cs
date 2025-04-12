using System.Collections;
using System.Collections.Generic;
using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Balance
{
    [CreateSingletonAsset("Assets/_Project/Configs/SO_DroneUpgrades.asset", "Drone Upgrades")]
    public class DroneUpgradesCollection : ScriptableObject, IReadOnlyList<DroneUpgrade>
    {
        [SerializeField] private DroneUpgrade[] _ugprades;
        
        public IEnumerator<DroneUpgrade> GetEnumerator() => ((IEnumerable<DroneUpgrade>)_ugprades).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => _ugprades.Length;
        public DroneUpgrade this[int index] => _ugprades[index];
    }
}