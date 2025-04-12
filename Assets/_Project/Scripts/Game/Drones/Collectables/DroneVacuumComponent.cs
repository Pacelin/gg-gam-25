using System;
using R3;
using UnityEngine;
using UnityEngine.Events;

namespace GGJam25.Game.Drones.Collectables
{
    public class DroneVacuumComponent : MonoBehaviour
    {
        [SerializeField] private GameObject[] _byLevel;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private LayerMask _collectablesMask;
        [SerializeField] private float _collectGrantDistance = 0.1f;
        [SerializeField] private UnityEvent _onCollectEvent;

        private GameObject _activeGO;
        private IDisposable _disposable;
        
        private void Awake()
        {
            foreach (var go in _byLevel)
                go.SetActive(false);
        }

        private void OnEnable()
        {
            _disposable = GameContext.DroneUpgrades.VacuumLevel.Subscribe(l =>
            {
                if (_activeGO)
                    _activeGO.SetActive(false);
                _activeGO = _byLevel[l];
                _activeGO.SetActive(true);
            });
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void Update()
        {
            var pos = _firePoint.position;
            var firstPoint = pos + Vector3.up * 5;
            var secondPoint = pos + Vector3.up * -5;
            var colliders = Physics.OverlapCapsule(firstPoint, secondPoint, 
                GameContext.DroneUpgrades.VacuumDistance.CurrentValue, _collectablesMask);
            foreach (var col in colliders)
            {
                var collectable = col.GetComponent<CollectableComponent>();
                if (!collectable)
                    continue;
                if (CanMagnet(collectable))
                    Magnet(collectable);
            }
        }

        private bool CanMagnet(CollectableComponent collectable)
        {
            var collectablePos = collectable.transform.position;
            if (Vector3.Distance(collectablePos, _firePoint.position) <= _collectGrantDistance)
                return true;
            var forward = _firePoint.forward;
            forward.y = 0;
            var toCollectable = collectablePos - _firePoint.position;
            toCollectable.y = 0;
            var angle = Vector3.Angle(forward.normalized, toCollectable.normalized);
            return angle <= GameContext.DroneUpgrades.VacuumAngle.CurrentValue;
        }

        private void Magnet(CollectableComponent collectable)
        {
            var targetPosition = _firePoint.position;
            var newPosition = Vector3.MoveTowards(collectable.transform.position, targetPosition,
                GameContext.DroneUpgrades.VacuumPower.CurrentValue * Time.deltaTime);
            collectable.transform.position = newPosition;
            if (newPosition == targetPosition)
            {
                collectable.Collect();
                _onCollectEvent?.Invoke();
            }
        }
    }
}