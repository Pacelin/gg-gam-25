using UnityEngine;

namespace GGJam25.Game.Drones
{
    [RequireComponent(typeof(Collider))]
    public class DoorEnterTrigger : MonoBehaviour
    {
        [SerializeField] private DoorComponent _door;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DroneComponent>(out _))
                GameContext.OctagonController.EnterDoor(_door);
        }
    }
}