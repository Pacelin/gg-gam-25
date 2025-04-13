using System;
using System.Collections.Generic;
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
            GameContext.CollectedKeys = new List<int>();
            GameContext.OctagonPlayer = new OctagonPlayer();

            GameContext.OctagonController = Object.Instantiate(CMS.Prefabs.OctagonController);
            GameContext.Level = Object.Instantiate(CMS.Prefabs.Level);
            GameContext.HUD = Object.Instantiate(CMS.Prefabs.HUD);
            GameContext.Shop = Object.Instantiate(CMS.Prefabs.Shop);
            
            GameContext.Level.Hub.Station.Spawn();
        }

        public void Tick()
        {
        }

        public void Dispose()
        {
            GameContext.OctagonPlayer.Stop();
            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}