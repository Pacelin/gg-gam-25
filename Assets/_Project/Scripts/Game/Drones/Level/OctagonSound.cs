using GGJam25.Game.Indicators;
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
            if (other.TryGetComponent<DroneTriggerPoint>(out _))
                GameContext.OctagonPlayer.Switch(_octagonEvent);
        }
    }
}