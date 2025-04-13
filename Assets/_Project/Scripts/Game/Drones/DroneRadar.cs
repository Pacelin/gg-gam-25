using GGJam25.Game.Drones.Collectables;
using R3;
using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public class DroneRadar : MonoBehaviour
    {
        [SerializeField] private GameObject _enabledState;
        [SerializeField] private GameObject _disabledState;
        [SerializeField] private Transform _arrowHub;
        [SerializeField] private Transform _arrowKey;

        private CompositeDisposable _disposables;

        private void Update()
        {
            if (!GameContext.DroneUpgrades.RadarEnabled.CurrentValue)
                return;

            CollectableKey nearbyKey = null;
            float distance = float.MaxValue;
            Vector3 vector = Vector3.zero;
            foreach (var key in GameContext.Level.CollectableKeys)
            {
                if (!key)
                    continue;
                var first = key.Octagon.position;
                var second = GameContext.OctagonController.ActiveOctagon.position;
                var curDistance = Vector3.Distance(first, second);
                if (curDistance < distance)
                {
                    nearbyKey = key;
                    distance = curDistance;
                    vector = first - second;
                }
            }
            _arrowKey.gameObject.SetActive(nearbyKey && distance > 0);
            if (nearbyKey)
            {
                vector.y = vector.z;
                vector.z = 0;
                _arrowKey.right = -vector.normalized;
            }
            
            _arrowHub.gameObject.SetActive(GameContext.OctagonController.ActiveOctagon != GameContext.Level.Hub.transform);
            if (GameContext.OctagonController.ActiveOctagon != GameContext.Level.Hub.transform)
            {
                vector = (GameContext.Level.Hub.transform.position - GameContext.OctagonController.ActiveOctagon.position);
                vector.y = vector.z;
                vector.z = 0;
                _arrowHub.right = -vector.normalized;
            }
        }
        
        private void OnEnable()
        {
            _disposables = new CompositeDisposable();
            GameContext.DroneUpgrades.RadarEnabled.Subscribe(enabled =>
            {
                _arrowHub.gameObject.SetActive(enabled);
                _arrowKey.gameObject.SetActive(enabled);
                _enabledState.SetActive(enabled);
                _disabledState.SetActive(!enabled);
            }).AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables?.Dispose();
        }
    }
}