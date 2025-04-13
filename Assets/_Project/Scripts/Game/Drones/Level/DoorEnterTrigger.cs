using GGJam25.Game.Indicators;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [RequireComponent(typeof(Collider))]
    public class DoorEnterTrigger : MonoBehaviour
    {
        [SerializeField] private DoorComponent _door;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DroneTriggerPoint>(out _))
                GameContext.OctagonController.EnterDoor(_door);
        }
    }
}