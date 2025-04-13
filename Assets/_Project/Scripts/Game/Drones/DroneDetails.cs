using R3;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneDetails : MonoBehaviour
    {
        [System.Serializable]
        public struct Engines
        {
            [SerializeField] private ParticleSystem[] _systems;

            public void Enable()
            {
                foreach(var sys in _systems)
                    sys.Play();
            }

            public void Disable()
            {
                foreach(var sys in _systems)
                    sys.Stop();
            }
        }

        [SerializeField] private Engines[] _engines;
        [SerializeField] private GameObject[] _speedlevels;
        [SerializeField] private GameObject _coldComponent;
        [SerializeField] private GameObject _hotComponent;
        [SerializeField] private GameObject _waterComponent;
        [SerializeField] private GameObject _sandComponent;

        private CompositeDisposable _disposables;

        public Engines GetEngine() => _engines[GameContext.DroneUpgrades.SpeedLevel.Value];
        
        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
            GameContext.DroneUpgrades.SpeedLevel.Subscribe(l =>
                {
                    for (int i = 0; i < _speedlevels.Length; i++)
                    {
                        if (_speedlevels[i] == null)
                            continue;
                        _speedlevels[i].SetActive(l == i);
                    }
                }).AddTo(_disposables);
            GameContext.DroneUpgrades.ColdUpgradeLevel.Subscribe(l =>
                _coldComponent.SetActive(l > 0)).AddTo(_disposables);
            GameContext.DroneUpgrades.HotUpgradeLevel.Subscribe(l =>
                _hotComponent.SetActive(l > 0)).AddTo(_disposables);
            GameContext.DroneUpgrades.WaterUpgradeLevel.Subscribe(l =>
                _waterComponent.SetActive(l > 0)).AddTo(_disposables);
            GameContext.DroneUpgrades.SandUpgradeLevel.Subscribe(l =>
                _sandComponent.SetActive(l > 0)).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables.Dispose();
        }
    }
}