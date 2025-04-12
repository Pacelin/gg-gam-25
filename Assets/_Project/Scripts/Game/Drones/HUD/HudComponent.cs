using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones.HUD
{
    public class HudComponent : MonoBehaviour
    {
        [SerializeField] private ScriptableTween _showDroneCanvasTween;
        [SerializeField] private ScriptableTween _hideDroneCanvasTween;

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