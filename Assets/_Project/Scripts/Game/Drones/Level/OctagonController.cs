using System;
using DG.Tweening;
using TSS.Core;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class OctagonController : MonoBehaviour
    {
        public Transform ActiveOctagon => _activeOctagon ? _activeOctagon : GameContext.Level.Hub.transform;
        
        [SerializeField] private CanvasGroup _fadeCanvas;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _moveCameraDuration;
        [SerializeField] private float _fadeDelay;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _fadeInterval;

        private Transform _activeOctagon;

        private void OnDisable()
        {
            DOTween.Kill(this);
        }
        
        public void EnterDoor(DoorComponent door)
        {
            DOTween.Kill(this);
            _activeOctagon = door.Neighbour.Octagon;
            var newCamPosition = door.Neighbour.Octagon.position + _cameraOffset;
            var newDronePosition = door.Neighbour.Entry.position;
            DOTween.Sequence(this)
                .AppendCallback(() =>
                {
                    GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.Lock();
                    GameContext.HUD.SetShopButtonActive(door.Neighbour.Octagon == GameContext.Level.Hub.transform);
                })
                .Append(SceneCameraProvider.MainCamera.transform.DOMove(newCamPosition, _moveCameraDuration))
                .Join(GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.transform.DOMove(newDronePosition, _moveCameraDuration))
                .AppendCallback(() =>
                {
                    GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.Unlock();
                    if (door.Neighbour.Octagon == GameContext.Level.Hub.transform)
                        GameContext.DroneStorage.CollectDrone();
                }).Play();
        }

        public void MoveCameraTo(Transform point, Action onFinished)
        {
            DOTween.Kill(this);
            var newCamPosition = point.position + _cameraOffset;
            _activeOctagon = GameContext.Level.Hub.transform;
            DOTween.Sequence(this)
                .AppendCallback(() =>
                {
                    if (GameContext.Level.Hub.Station.ActiveDrone.CurrentValue)
                        GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.Lock();
                    _fadeCanvas.gameObject.SetActive(true);
                })
                .Append(SceneCameraProvider.MainCamera.transform.DOMove(newCamPosition, _moveCameraDuration))
                .Join(_fadeCanvas.DOFade(1, _fadeDuration).SetDelay(_fadeDelay))
                .AppendInterval(_fadeInterval)
                .Append(_fadeCanvas.DOFade(0, _fadeDuration))
                .AppendCallback(() =>
                {
                    if (GameContext.Level.Hub.Station.ActiveDrone.CurrentValue)
                        GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.Unlock();
                    _fadeCanvas.gameObject.SetActive(false);
                    onFinished?.Invoke();
                }).Play();
        }
    }
}