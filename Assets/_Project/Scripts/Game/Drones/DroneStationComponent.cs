using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJam25.Game.Drones
{
    public class DroneStart : MonoBehaviour, IPointerClickHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
            
        }
    }
    
    public class DroneStationPaths : MonoBehaviour
    {
        //[SerializeField] private 
    }
    
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