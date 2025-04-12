using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [CreateSingletonAsset("Assets/_Project/Configs/Upgrades/SO_DroneVacuum.asset", "Drone Vacuum Upgrade")]
    public class DroneVacuumUpgrade : DroneUpgrade
    {
        public float[] Power => _power;
        public float[] Width => _width;
        public float[] Distance => _distance;
        
        [Space] 
        [SerializeField] private string _powerValueStr = "Мощность: ";
        [SerializeField] private string _widthValueStr = "Ширина: ";
        [SerializeField] private string _distanceValueStr = "Дальность: ";
        [SerializeField] private float[] _power;
        [SerializeField] private float[] _width;
        [SerializeField] private float[] _distance;

        protected override void OnView(int currentLevel, DroneUpgradeView view, bool isMax)
        {
            base.OnView(currentLevel, view);
            if (isMax)
            {
                view.AddInfo(_powerValueStr, _power[currentLevel].ToString("0.#"));
                view.AddInfo(_widthValueStr, _width[currentLevel].ToString("0.#"));
                view.AddInfo(_distanceValueStr, _distance[currentLevel].ToString("0.#"));
            }
            else
            {
                view.AddInfo(_powerValueStr, 
                    _power[currentLevel].ToString("0.#"), _power[currentLevel + 1].ToString("0.#"));
                view.AddInfo(_widthValueStr, 
                    _width[currentLevel].ToString("0.#"), _width[currentLevel + 1].ToString("0.#"));
                view.AddInfo(_distanceValueStr, 
                    _distance[currentLevel].ToString("0.#"), _distance[currentLevel + 1].ToString("0.#"));
            }
        }
    }
}