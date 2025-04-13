using TSS.Audio;
using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    public class CollectableKey : CollectableComponent
    {
        public Transform Octagon => _octagon;
        
        [SerializeField] private Transform _octagon;
        [SerializeField] private int _keyIndex;
        
        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        protected override void OnCollect()
        {
            AudioSystem.Game_KeyCollect.PlayOneShot();
            GameContext.CollectedKeys.Add(_keyIndex);
        }
    }
}