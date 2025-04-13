using TSS.Audio;
using Unity.VisualScripting;

namespace GGJam25.Game.Drones
{
    public class OctagonPlayer
    {
        private SoundEventInstance _instance;
        private string _lastId;

        public void Stop()
        {
            if (_instance != null)
            {
                _instance.Stop(true);
                _instance.Release();
            }
        }
        
        public void Switch(SoundEvent evt)
        {
            if (_lastId == evt.GUIO)
                return;
            if (_instance != null)
            {
                _instance.Stop(true);
                _instance.Release();
            }

            _instance = evt.CreateInstance();
            _instance.Start();
            _lastId = evt.GUIO;
        }
    }
}