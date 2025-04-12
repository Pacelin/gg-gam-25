using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LudumDare57.Game
{
    public class GameScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameManager>();
        }
    }

    public class LevelComponent : MonoBehaviour
    {
        [SerializeField] private HubComponent _hub;
        [SerializeField] private RoomComponent[] _rooms;
    }
    
    public class RoomComponent : MonoBehaviour
    {
        [SerializeField] private Transform _doors;
    }

    public class HubComponent : MonoBehaviour
    {
        
    }
}