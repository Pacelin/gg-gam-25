using System.Threading;

namespace LudumDare57.Game
{
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
        public static int DroneSpeedLevel { get; set; }
    }
}