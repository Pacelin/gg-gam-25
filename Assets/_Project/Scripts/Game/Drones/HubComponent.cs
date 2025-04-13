using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class HubComponent : MonoBehaviour
    {
        public DroneStationComponent Station => _station;
        
        [SerializeField] private DroneStationComponent _station;
    }
}