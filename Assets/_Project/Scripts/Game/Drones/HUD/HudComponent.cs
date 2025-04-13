using System;
using GGJam25.UI;
using R3;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones.HUD
{
    public class HudComponent : MonoBehaviour
    {
        [SerializeField] private ScriptableButton _shopButton;
        [SerializeField] private ScriptableTween _showDroneCanvasTween;
        [SerializeField] private ScriptableTween _hideDroneCanvasTween;

        private IDisposable _disposable;

        public void SetShopButtonActive(bool active) => _shopButton.gameObject.SetActive(active);
        
        private void OnEnable()
        {
            _disposable = _shopButton.ObserveFeedbackStart().Subscribe(_ => 
                GameContext.Shop.Switch());
        }

        private void OnDisable() => _disposable?.Dispose();

        public void ShowDroneCanvas()
        {
            if (_hideDroneCanvasTween.IsPlaying)
                _hideDroneCanvasTween.Kill();
            _showDroneCanvasTween.Play();
        }

        public void HideDroneCanvas()
        {
            if (_showDroneCanvasTween.IsPlaying)
                _showDroneCanvasTween.Kill();
            _hideDroneCanvasTween.Play();
        }
    }
}