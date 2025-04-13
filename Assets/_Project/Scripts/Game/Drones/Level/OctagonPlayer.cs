using TSS.Audio;

namespace GGJam25.Game.Drones
{
    public class OctagonPlayer
    {
        private SoundEventInstance _instance;

        public void Switch(SoundEvent evt)
        {
            if (_instance != null)
            {
                _instance.Stop(true);
                _instance.Release();
            }

            _instance = evt.CreateInstance();
            _instance.Start();
        }
    }
}