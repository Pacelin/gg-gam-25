using UnityEngine;

namespace GGJam25.Game.Drones
{
    public abstract class DroneUpgrade : ScriptableObject
    {
        [SerializeField] private string _name;
        [TextArea] 
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int[] _prices;

        public int GetPrice(int currentLevel) => _prices[currentLevel];
        public bool IsMax(int currentLevel) => currentLevel >= _prices.Length;
        
        public void OnView(int currentLevel, DroneUpgradeView view)
        {
            view.SetIcon(_icon);
            view.SetName(_name, currentLevel);
            view.SetDescription(_description);
            var isMax = currentLevel >= _prices.Length;
            view.SetMax(isMax);
            if (!isMax)
                view.SetPrice(_prices[currentLevel]);
            OnView(currentLevel, view, isMax);
        }

        protected abstract void OnView(int currentLevel, DroneUpgradeView view, bool isMax);
    }
}