using TSS.Audio;
using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    public class CollectableResource : CollectableComponent
    {
        [SerializeField] private int _resourceAmount;

        protected override void OnCollect()
        {
            AudioSystem.Game_ResourceCollect.PlayOneShot();
            GameContext.DroneStorage.AddResourceForDrone(_resourceAmount);
        }
    }
}