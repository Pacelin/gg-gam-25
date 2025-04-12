using System;
using System.Threading;
using TSS.Core;
using VContainer.Unity;

namespace LudumDare57.Game
{
    public class GameManager : IInitializable, ITickable, IDisposable
    {
        private CancellationTokenSource _cts;

        public void Initialize()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
            GameContext.CancellationToken = _cts.Token;
            GameContext.DroneSpeedLevel = 0;
        }

        public void Tick()
        {
        }

        public void Dispose()
        {
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}