using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class LevelComponent : MonoBehaviour
    {
        public HubComponent Hub => _hub;

        [SerializeField] private HubComponent _hub;
    }
}