using System;
using TSS.Tweening;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GGJam25.Game
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

    [RequireComponent(typeof(Collider))]
    public class DoorOpenTrigger : MonoBehaviour
    {
        [SerializeField] private DoorComponent _component;
        private void OnValidate() => GetComponent<Collider>().isTrigger = true;

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
    
    public class DoorComponent : MonoBehaviour
    {
        [SerializeField] private ScriptableTween _openTween;
        [SerializeField] private ScriptableTween _closeTween;
        
        

    }
    
    public class RoomComponent : MonoBehaviour
    {
        [SerializeField] private Transform _doors;
    }

    public class HubComponent : MonoBehaviour
    {
        
    }
}