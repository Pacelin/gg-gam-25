using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJam25.Game.Drones
{
    public class DroneStationComponent : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<DroneComponent> ActiveDrone => _activeDrone;

        [SerializeField] private Transform _droneSpawnPoint;
        [SerializeField] private ScriptableTween _spawnTween;

        private ReactiveProperty<DroneComponent> _activeDrone;

        public void Death()
        {
            GameContext.HUD.HideDroneCanvas();
        }
        
        public void Spawn()
        {
            GameContext.HUD.ShowDroneCanvas();
        }
        
        public void Revive()
        {
            Destroy(_activeDrone.Value.gameObject);
        }
    }
}