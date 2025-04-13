// Auto-generated code. Reference: "Packages/com.tss.cms/Editor/CMSGenerator.cs"

// ReSharper disable RedundantUsingDirective
#pragma warning disable CS1998

using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using JetBrains.Annotations;
using UnityEngine;
using GGJam25.Game.Drones;
using GGJam25.Game.Drones.HUD;
using TSS.Core;

namespace TSS.ContentManagement
{
    [PublicAPI]
    [UsedImplicitly]
    [RuntimeOrder(ERuntimeOrder.SystemRegistration)]
    public class CMS : IRuntimeLoader
    {
 

        public async UniTask Initialize(CancellationToken cancellationToken)
        {
			await Scenes.Initialize(cancellationToken);
			await Upgrades.Initialize(cancellationToken);
			await Prefabs.Initialize(cancellationToken);
        }

        public void Dispose() { }

		[PublicAPI]
		public static class Scenes
		{
			public const string MainMenu = "Assets/Scenes/1_Menu.unity";
			public const string Game = "Assets/Scenes/2_Game.unity";

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
			}
		}
		[PublicAPI]
		public static class Upgrades
		{
			public static DroneSpeedUpgrade Speed { get; private set; }
			public static DroneStorageUpgrade Storage { get; private set; }
			public static DroneVacuumUpgrade Vacuum { get; private set; }
			public static DroneStabilizationUpgrade Stabilization { get; private set; }
			public static DroneRadarUpgrade Radar { get; private set; }

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
				Speed = await Addressables.LoadAssetAsync<DroneSpeedUpgrade>("Drone Speed Upgrade")
					.ToUniTask(cancellationToken: cancellationToken);
				Storage = await Addressables.LoadAssetAsync<DroneStorageUpgrade>("Drone Storage Upgrade")
					.ToUniTask(cancellationToken: cancellationToken);
				Vacuum = await Addressables.LoadAssetAsync<DroneVacuumUpgrade>("Drone Vacuum Upgrade")
					.ToUniTask(cancellationToken: cancellationToken);
				Stabilization = await Addressables.LoadAssetAsync<DroneStabilizationUpgrade>("Drone Stabilization Upgrade")
					.ToUniTask(cancellationToken: cancellationToken);
				Radar = await Addressables.LoadAssetAsync<DroneRadarUpgrade>("Drone Radar Upgrade")
					.ToUniTask(cancellationToken: cancellationToken);
			}
		}
		[PublicAPI]
		public static class Prefabs
		{
			public static HudComponent HUD { get; private set; }
			public static DroneComponent Drone { get; private set; }
			public static LevelComponent Level { get; private set; }
			public static OctagonController OctagonController { get; private set; }
			public static DroneShop Shop { get; private set; }

			public static async UniTask Initialize(CancellationToken cancellationToken)
			{
				HUD = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/GUI/P_HUD.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<HudComponent>();
				Drone = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Rooms/P_Drone.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<DroneComponent>();
				Level = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/Rooms/P_Level.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<LevelComponent>();
				OctagonController = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/GUI/P_Octagon_Controller.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<OctagonController>();
				Shop = (await Addressables.LoadAssetAsync<GameObject>("Assets/_Project/Content/GUI/P_Shop.prefab")
					.ToUniTask(cancellationToken: cancellationToken)).GetComponent<DroneShop>();
			}
		}
    }
}