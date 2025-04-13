using System.Threading;
using GGJam25.Game.Drones;
using GGJam25.Game.Drones.HUD;

namespace GGJam25.Game
{
    [System.Serializable]
    
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        public static DroneUpgrades DroneUpgrades { get; set; }
        public static DroneStorage DroneStorage { get; set; }

        public static LevelComponent Level { get; set; }
        public static HudComponent HUD { get; set; }
        public static DroneShop Shop { get; set; }
        public static OctagonController OctagonController { get; set; }
        public static OctagonPlayer OctagonPlayer { get; set; }
        
        public static int CollectedKeys { get; set; }
    }
}