using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneStationComponent : MonoBehaviour
    {
        public DroneComponent ActiveDrone => _activeDrone;
        private DroneComponent _activeDrone;
        
        public void Revive()
        {
            
        }
    }
}