using TSS.Audio;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [RequireComponent(typeof(Collider))]
    public class OctagonSound : MonoBehaviour
    {
        [SerializeField] private SoundEvent _octagonEvent;

        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DroneComponent>(out _))
                GameContext.OctagonPlayer.Switch(_octagonEvent);
        }
    }
}