using R3;
using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public class DroneHealth
    {
        public Observable<Unit> OnKill => _onKill;

        public ReadOnlyReactiveProperty<float> Water => _water;
        public ReadOnlyReactiveProperty<float> Temperature => _temperature;

        private Subject<Unit> _onKill = new();
        private ReactiveProperty<float> _water = new(0);
        private ReactiveProperty<float> _temperature = new(0);
        private bool _killed = false;
        
        public void ApplyWaterInfluence(float influence)
        {
            if (_killed)
                return;
            _water.Value = Mathf.Clamp(_water.Value + influence, -1, 1);
            if (_water.Value >= 1 || _water.Value <= -1)
            {
                _killed = true;
                _onKill.OnNext(Unit.Default);
            }
        }

        public void ApplyTemperatureInfluence(float influence)
        {
            if (_killed)
                return;
            _temperature.Value = Mathf.Clamp(_temperature.Value + influence, -1, 1);
            if (_temperature.Value >= 1 || _temperature.Value <= -1)
            {
                _killed = true;
                _onKill.OnNext(Unit.Default);
            }
        }
    }
}