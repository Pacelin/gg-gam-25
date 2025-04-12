using System.Collections.Generic;
using TSS.Core;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneInfluenceReciever : MonoBehaviour
    {
        [SerializeField] private DroneComponent _drone;

        private readonly List<DroneInfluencer> _influencers = new();
        
        private void OnValidate()
        {
            if (!_drone)
                _drone = GetComponent<DroneComponent>();
        }

        private void Update()
        {
            if (Runtime.IsPaused)
                return;
            var water = 0f;
            var temp = 0f;
            foreach (var influencer in _influencers)
            {
                water += influencer.WaterInfluence;
                temp += influencer.TemperatureInfluence;
            }

            water += Mathf.MoveTowards(_drone.Health.Water.CurrentValue, 0,
                GameContext.DroneUpgrades.Stabilization.CurrentValue * Time.deltaTime) - _drone.Health.Water.CurrentValue;
            temp += Mathf.MoveTowards(_drone.Health.Temperature.CurrentValue, 0,
                        GameContext.DroneUpgrades.Stabilization.CurrentValue * Time.deltaTime) - _drone.Health.Temperature.CurrentValue;
            
            _drone.Health.ApplyWaterInfluence(water);
            _drone.Health.ApplyTemperatureInfluence(temp);
        }

        private void OnTriggerEnter(Collider other)
        {
            var influencer = other.GetComponent<DroneInfluencer>();
            if (influencer)
                _influencers.Add(influencer);
        }

        private void OnTriggerExit(Collider other)
        {
            var influencer = other.GetComponent<DroneInfluencer>();
            if (influencer)
                _influencers.Remove(influencer);
        }
    }
}