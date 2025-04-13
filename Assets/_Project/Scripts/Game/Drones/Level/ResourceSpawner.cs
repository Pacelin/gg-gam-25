using System;
using GGJam25.Game.Drones.Collectables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GGJam25.Game.Drones
{
    public class ResourceSpawner : MonoBehaviour
    {
        [SerializeField] private Color _color;
        [SerializeField] private Vector2 _cooldownRange;
        [SerializeField] private CollectableResource _resourcePrefab;

        private CollectableResource _instance;
        private float _cooldown;
        
        private void Update()
        {
            if (_instance)
                return;
            _cooldown -= Time.deltaTime;
            if (_cooldown <= 0)
            {
                _instance = Instantiate(_resourcePrefab, transform.position, Random.rotation);
                _cooldown = Random.Range(_cooldownRange.x, _cooldownRange.y);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            Gizmos.DrawSphere(transform.position, 0.2f);
        }
    }
}