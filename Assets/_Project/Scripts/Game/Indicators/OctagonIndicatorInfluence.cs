using R3;

namespace GGJam25.Game.Indicators
{
    public class 
    
    public class HealthIndicator
    {
        private ReactiveProperty<float> _water = new(0);
        private ReactiveProperty<float> _temperature = new(0);

        public void ApplyWaterInfluence(float influence)
        {
            _water.Value += influence;
        }

        public void ApplyTemperatureInfluence(float influence)
        {
            _temperature.Value += influence;
        }
    }
    
    public class OctagonIndicatorInfluence
    {
        
    }
}