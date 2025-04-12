using UnityEngine;

namespace GGJam25.Game.Drones
{
    [RequireComponent(typeof(Collider))]
    public class DroneInfluencer : MonoBehaviour
    {
        public float WaterInfluence => _waterInfluence;
        public float TemperatureInfluence => _temperatureInfluence;
        
        [SerializeField] private float _waterInfluence;
        [SerializeField] private float _temperatureInfluence;

        private void OnValidate()
        {
            GetComponent<Collider>().isTrigger = true;
        }
    }
}