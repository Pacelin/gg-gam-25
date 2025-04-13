using System;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    [RequireComponent(typeof(Collider))]
    public class DroneInfluencer : MonoBehaviour
    {
        public float WaterInfluence => _waterInfluence;
        public float TemperatureInfluence => _temperatureInfluence;
        
        [SerializeField] private float _temperatureInfluence;
        [SerializeField] private float _waterInfluence;

        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DroneInfluenceReciever>(out var reciever))
                reciever.Register(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<DroneInfluenceReciever>(out var reciever))
                reciever.Unregister(this);
        }
    }
}