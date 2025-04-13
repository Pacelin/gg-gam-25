using GGJam25.Game.Drones.Collectables;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class LevelComponent : MonoBehaviour
    {
        public HubComponent Hub => _hub;
        public CollectableKey[] CollectableKeys => _collectableKeys;

        [SerializeField] private HubComponent _hub;
        [SerializeField] private CollectableKey[] _collectableKeys;
    }
}