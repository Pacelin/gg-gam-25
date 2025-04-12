using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Balance
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneSpeed.asset", "Drone Speed Upgrade")]
    public class DroneSpeedUpgrade : DroneUpgrade
    {
        public float[] LinearSpeeds => _linearSpeeds;
        public float[] AngularSpeeds => _angularSpeeds;
        
        [Space] 
        [SerializeField] private string _linearSpeedValueStr = "Скорость: ";
        [SerializeField] private string _angularSpeedValueStr = "Скорость поворота: ";
        [SerializeField] private float[] _linearSpeeds;
        [SerializeField] private float[] _angularSpeeds;

        protected override void OnView(int currentLevel, DroneUpgradeView view, bool isMax)
        {
            base.OnView(currentLevel, view);
            if (isMax)
            {
                view.AddInfo(_linearSpeedValueStr, _linearSpeeds[currentLevel].ToString());
                view.AddInfo(_angularSpeedValueStr, _angularSpeeds[currentLevel].ToString());
            }
            else
            {
                view.AddInfo(_linearSpeedValueStr, _linearSpeeds[currentLevel].ToString(),
                    _linearSpeeds[currentLevel + 1].ToString());
                view.AddInfo(_angularSpeedValueStr, _angularSpeeds[currentLevel].ToString(),
                    _angularSpeeds[currentLevel + 1].ToString());
            }
        }
    }
}