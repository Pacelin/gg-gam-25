using System;
using DG.Tweening;
using TSS.Core;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class OctagonController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _fadeCanvas;
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private float _moveCameraDuration;
        [SerializeField] private float _fadeDelay;
        [SerializeField] private float _fadeDuration;
        [SerializeField] private float _fadeInterval;
        
        private void OnDisable()
        {
            DOTween.Kill(this);
        }
        
        public void EnterDoor(DoorComponent door)
        {
            DOTween.Kill(this);
            var newCamPosition = door.Neighbour.Octagon.position + _cameraOffset;
            var newDronePosition = door.Neighbour.Entry.position;
            DOTween.Sequence(this)
                .AppendCallback(() =>
                {
                    GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.Lock();
                    _fadeCanvas.gameObject.SetActive(true);
                })
                .Append(SceneCameraProvider.MainCamera.transform.DOMove(newCamPosition, _moveCameraDuration))
                .Join(_fadeCanvas.DOFade(1, _fadeDuration).SetDelay(_fadeDelay))
                .AppendCallback(() =>
                {
                    GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.SetPosition(newDronePosition);
                })
                .AppendInterval(_fadeInterval)
                .Append(_fadeCanvas.DOFade(0, _fadeDuration))
                .AppendCallback(() =>
                {
                    GameContext.Level.Hub.Station.ActiveDrone.CurrentValue.Unlock();
                    _fadeCanvas.gameObject.SetActive(false);
                    if (door.Neighbour.Octagon == GameContext.Level.Hub.transform)
                        GameContext.DroneStorage.CollectDrone();
                });
        }

        public void MoveCameraTo(Transform point, Action onFinished)
        {
            DOTween.Kill(this);
            var newCamPosition = point.position + _cameraOffset;
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
                });
        }
    }
}