using Cysharp.Threading.Tasks;
using R3;
using TSS.ContentManagement;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DoorComponent : MonoBehaviour
    {
        public Transform Octagon => _octagon;
        public Transform Entry => _entry;
        public DoorComponent Neighbour => _neighbour;
        
        [SerializeField] private Transform _octagon;
        [SerializeField] private Transform _entry;
        [SerializeField] private DoorComponent _neighbour;
        [SerializeField] private ScriptableTween _openTween;
        [SerializeField] private ScriptableTween _closeTween;

        public void Open()
        {
            if (_closeTween.IsPlaying)
                _closeTween.Kill();
            _openTween.Play();
        }

        public void Close()
        {
            if (_openTween.IsPlaying)
                _openTween.Kill();
            _closeTween.Play();
        }
    }

    public class DroneStationComponent : MonoBehaviour
    {
        public ReadOnlyReactiveProperty<DroneComponent> ActiveDrone => _activeDrone;

        [SerializeField] private Transform _droneSpawnPoint;
        [SerializeField] private ScriptableTween _spawnTween;

        private readonly ReactiveProperty<DroneComponent> _activeDrone = new();

        public void Death()
        {
            _activeDrone.Value.Lock();
            GameContext.HUD.HideDroneCanvas();
        }

        public void Spawn()
        {
            _activeDrone.Value = Instantiate(CMS.Prefabs.Drone, _droneSpawnPoint);
            _activeDrone.Value.transform.localPosition = Vector3.zero;
            _activeDrone.Value.Lock();
            _spawnTween.Play();
            _spawnTween.WaitWhilePlay().ContinueWith(() =>
            {
                _activeDrone.Value.transform.SetParent(null);
                _activeDrone.Value.Unlock();
            });
            GameContext.HUD.ShowDroneCanvas();
        }
        
        public void Revive()
        {
            var oldDrone = _activeDrone.Value.gameObject;
            GameContext.OctagonController.MoveCameraTo(GameContext.Level.Hub.transform, () =>
            {
                Destroy(oldDrone);
                Spawn();
            });
        }
    }
}