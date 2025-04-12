using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    [RequireComponent(typeof(Collider))]
    public abstract class CollectableComponent : MonoBehaviour
    {
        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        public void Collect()
        {
            OnCollect();
            Destroy(gameObject);
        }
        
        protected abstract void OnCollect();
    }
}