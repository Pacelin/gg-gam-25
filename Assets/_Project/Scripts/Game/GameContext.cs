﻿using System.Threading;

namespace LudumDare57.Game
{
    public static class GameContext
    {
        public static CancellationToken CancellationToken { get; set; }
    }
}