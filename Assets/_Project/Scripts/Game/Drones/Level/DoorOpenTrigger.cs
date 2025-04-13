using GGJam25.Game.Indicators;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [RequireComponent(typeof(Collider))]
    public class DoorOpenTrigger : MonoBehaviour
    {
        [SerializeField] private DoorComponent _door;

        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DroneTriggerPoint>(out _))
                _door.Open();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<DroneTriggerPoint>(out _))
                _door.Close();
        }
    }
}