using System;
using R3;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public class ArrowMeterDroneBar : DroneBar
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _rotation0;
        [SerializeField] private float _rotationAmplitude;
        [SerializeField] private ScriptableTween _warningTween;
        
        private IDisposable _dipsosable;
        private float _lastFill;
        
        public override void Initialize(Observable<float> fill)
        {
            _dipsosable?.Dispose();
            _dipsosable = fill.Subscribe(f =>
            {
                if (_lastFill != 0 &&
                    Mathf.Abs(f) > Mathf.Abs(_lastFill) &&
                    Mathf.Abs(f) > 0.6f)
                {
                    if (!_warningTween.IsPlaying)
                        _warningTween.Play();
                }
                else
                {
                    if (_warningTween.IsPlaying)
                        _warningTween.Kill(true);
                }
                var fromRotation = _rotation0 + _rotationAmplitude;
                var toRotation = _rotation0 - _rotationAmplitude;
                var t = (f + 1) / 2f;
                _origin.rotation = Quaternion.Euler(0, 0, Mathf.Lerp(fromRotation, toRotation, t));
                _lastFill = f;
            });
        }

        private void OnDestroy() => _dipsosable?.Dispose();
    }
}