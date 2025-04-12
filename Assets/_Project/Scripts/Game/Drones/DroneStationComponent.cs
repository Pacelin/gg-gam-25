using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJam25.Game.Drones
{
    public class DroneStart : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private ScriptableTween _defaultState;
        [SerializeField] private ScriptableTween _hoverState;
        
        public void OnPointerClick(PointerEventData eventData)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_defaultState.IsPlaying)
                _defaultState.Kill();
            _hoverState.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_hoverState.IsPlaying)
                _hoverState.Kill();
            _defaultState.Play();
        }
    }
    
    public class DroneStationComponent : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<DroneComponent> ActiveDrone => _activeDrone;

        [SerializeField] private Transform _droneSpawnPoint;
        
        private ReactiveProperty<DroneComponent> _activeDrone;

        public void Spawn()
        {
            
        }
        
        public void Revive()
        {
            
        }
    }
}