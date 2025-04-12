using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJam25.Game.Drones
{
    public class DroneStationComponent : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<DroneComponent> ActiveDrone => _activeDrone;

        [SerializeField] private Transform _droneSpawnPoint;
        
        private ReactiveProperty<DroneComponent> _activeDrone;

        public void Spawn()
        {
            
        }
        
        public void Revive()
        {
            
        }
    }
}