using System;
using System.Threading;
using GGJam25.Game.Drones;
using TSS.ContentManagement;
using TSS.Core;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace GGJam25.Game
{
    public class GameManager : IInitializable, ITickable, IDisposable
    {
        private CancellationTokenSource _cts;

        public void Initialize()
        {
            _cts = CancellationTokenSource.CreateLinkedTokenSource(Runtime.CancellationToken);
            GameContext.CancellationToken = _cts.Token;
            GameContext.DroneUpgrades = new DroneUpgrades();
            GameContext.DroneStorage = new DroneStorage();
            GameContext.CollectedKeys = 0;

            GameContext.HUD = Object.Instantiate(CMS.Prefabs.HUD);
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