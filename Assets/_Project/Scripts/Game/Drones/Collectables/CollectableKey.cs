using TSS.Audio;
using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    public class CollectableKey : CollectableComponent
    {
        public Transform Octagon => _octagon;
        
        [SerializeField] private Transform _octagon;
        
        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        protected override void OnCollect()
        {
            AudioSystem.Game_KeyCollect.PlayOneShot();
            GameContext.CollectedKeys++;
        }
    }
}