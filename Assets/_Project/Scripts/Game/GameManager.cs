using System;
using System.Threading;
using VContainer.Unity;

namespace LudumDare57.Game
{
    public class GameManager : IInitializable, ITickable, IDisposable
    {
        private CancellationTokenSource _cts;

        public void Initialize()
        {
            GameContext.CancellationToken = _cts.Token;
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