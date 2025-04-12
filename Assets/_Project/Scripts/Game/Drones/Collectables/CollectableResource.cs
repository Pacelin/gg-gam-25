using TSS.Audio;
using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    [RequireComponent(typeof(Collider))]
    public class CollectableResource : CollectableComponent
    {
        [SerializeField] private int _resourceAmount;

        private void OnValidate() => GetComponent<Collider>().isTrigger = true;


        protected override void OnCollect()
        {
            AudioSystem.Game_ResourceCollect.PlayOneShot();
            GameContext.DroneStorage.AddResourceForDrone(_resourceAmount);
        }
    }
}