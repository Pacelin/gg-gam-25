using TSS.Audio;
using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    public class CollectableKey : CollectableComponent
    {
        private void OnValidate() => GetComponent<Collider>().isTrigger = true;


        protected override void OnCollect()
        {
            AudioSystem.Game_KeyCollect.PlayOneShot();
            GameContext.CollectedKeys++;
        }
    }
}