using System.Threading;
using GGJam25.Game.Balance;

namespace GGJam25.Game
{
    [System.Serializable]
    
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        public static DroneUpgrades DroneUpgrades { get; set; }
    }
}