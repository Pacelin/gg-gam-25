using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game.Balance
{
    [CreateSingletonAsset("Assets/_Project/Configs/SO_GameBalance.asset", "Game Balance")]
    public class GameBalanceConfig : ScriptableObject
    {
        public DroneSpeedUpgrade[] Speed => _speed;
        public DroneStorageUpgrade[] Storage => _storage;
        
        [Header("Upgrades")] 
        [SerializeField] private DroneSpeedUpgrade[] _speed;
        [SerializeField] private DroneStorageUpgrade[] _storage;
    }
}