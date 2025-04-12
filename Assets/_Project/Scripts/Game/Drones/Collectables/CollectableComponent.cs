using UnityEngine;

namespace GGJam25.Game.Drones.Collectables
{
    public abstract class CollectableComponent : MonoBehaviour
    {
        public void Collect()
        {
            OnCollect();
            Destroy(gameObject);
        }
        
        protected abstract void OnCollect();
    }
}