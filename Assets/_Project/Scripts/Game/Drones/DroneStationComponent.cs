using R3;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneStationComponent : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<DroneComponent> ActiveDrone => _activeDrone;
        private ReactiveProperty<DroneComponent> _activeDrone;
        
        public void Revive()
        {
            
        }
    }
}