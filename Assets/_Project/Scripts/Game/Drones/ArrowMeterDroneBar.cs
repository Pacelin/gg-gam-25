using System;
using R3;
using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public class ArrowMeterDroneBar : MonoBehaviour
    {
        [SerializeField] private Transform _origin;
        [SerializeField] private float _rotation0;
        [SerializeField] private float _rotationAmplitude;
        
        private IDisposable _dipsosable;
        
        public void Initialize(Observable<float> fill)
        {
            _dipsosable?.Dispose();
            _dipsosable = fill.Subscribe(f =>
            {
                var fromRotation = _rotation0 - _rotationAmplitude;
                var toRotation = _rotation0 + _rotationAmplitude;
                var t = (f + 1) / 2f;
                _origin.rotation = Quaternion.Euler(0, 0, t);
            });
        }

        private void OnDestroy() => _dipsosable?.Dispose();
    }
}