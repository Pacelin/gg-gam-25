using UnityEngine;

namespace GGJam25.Game.Balance
{
    public class DroneUpgradeView : MonoBehaviour
    {
        public void SetName(string upgradeName){}
        public void SetDescription(string upgradeDescription){}
        public void SetIcon(Sprite upgradeIcon){}
        public void SetPrice(int price){}
        public void SetMax(bool max){}
        public void AddInfo(string valueName, string curValue){}
        public void AddInfo(string valueName, string curValue, string nextValue){}
    }
}