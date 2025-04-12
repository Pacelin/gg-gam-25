using System.Collections;
using System.Collections.Generic;
using R3;
using TSS.ContentManagement;
using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Balance
{
    public class DroneStorageUpgrade : DroneUpgrade
    {
        [Space] 
        [SerializeField] private int[] _capacities;

        public override void OnBuy(int level)
        {
            
        }
    }

    public class DroneUpgrades
    {
        public ReactiveProperty<int> SpeedLevel => _speedLevel;
        public ReactiveProperty<int> StorageLevel => _storageLevel;

        public ReactiveProperty<int> DroneLinearSpeed => _droneLinearSpeed;
        public ReactiveProperty<int> DroneAngularSpeed => _droneAngularSpeed;
        public ReactiveProperty<int> DroneStorageCapacity => _droneStorageCapacity;

        private readonly ReactiveProperty<int> _speedLevel = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _storageLevel = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _droneLinearSpeed = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _droneAngularSpeed = new ReactiveProperty<int>(0);
        private readonly ReactiveProperty<int> _droneStorageCapacity = new ReactiveProperty<int>(0);
    }

    [CreateSingletonAsset("Assets/_Project/Configs/SO_DroneUpgrades.asset", "Drone Upgrades")]
    public class DroneUpgradesCollection : ScriptableObject, IReadOnlyList<DroneUpgrade>
    {
        [SerializeField] private DroneUpgrade[] _ugprades;
        
        public IEnumerator<DroneUpgrade> GetEnumerator() => ((IEnumerable<DroneUpgrade>)_ugprades).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public int Count => _ugprades.Length;
        public DroneUpgrade this[int index] => _ugprades[index];
    }

    public class DroneUpgradeView : MonoBehaviour
    {
        public void SetName(string upgradeName){}
        public void SetDescription(string upgradeDescription){}
        public void SetIcon(Sprite upgradeIcon){}
        public void SetPrice(int price){}
        public void SetMax(bool max){}
        public void AddInfo(string valueName, string curValue, string nextValue){}
    }
    
    public abstract class DroneUpgrade : ScriptableObject
    {
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        
        public int[] Prices => _prices;
        
        [SerializeField] private string _name;
        [TextArea] 
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int[] _prices;

        public abstract void OnBuy(int newlevel, DroneUpgrades upgrades);

        public virtual void OnView(int currentLevel, DroneUpgradeView view)
        {
            view.SetIcon(_icon);
            view.SetName(_name);
            view.SetDescription(_description);
            var isMax = currentLevel >= _prices.Length;
            view.SetMax(isMax);
            if (!isMax)
                view.SetPrice(_prices[currentLevel]);
        }
    }
}