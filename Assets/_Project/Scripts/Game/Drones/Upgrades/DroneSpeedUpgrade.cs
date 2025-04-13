using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
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
            if (isMax)
            {
                view.AddInfo(_linearSpeedValueStr, _linearSpeeds[currentLevel].ToString("0.#"));
                view.AddInfo(_angularSpeedValueStr, _angularSpeeds[currentLevel].ToString("0.#"));
            }
            else
            {
                view.AddInfo(_linearSpeedValueStr, _linearSpeeds[currentLevel].ToString("0.#"),
                    _linearSpeeds[currentLevel + 1].ToString("0.#"));
                view.AddInfo(_angularSpeedValueStr, _angularSpeeds[currentLevel].ToString("0.#"),
                    _angularSpeeds[currentLevel + 1].ToString("0.#"));
            }
        }
    }
}