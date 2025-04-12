using TSS.Utils;
using UnityEngine;

namespace GGJam25.Game
{
    [CreateSingletonAsset("Assets/_Project/Configs/SO_ResourcesStorage.asset", "Resources Storage Config")]
    public class ResourcesStorageConfig : ScriptableObject
    {
        [SerializeField] private int[] _capacityPerLevel;

        public int GetCapacity(int levelIndex) => _capacityPerLevel[levelIndex];
    }
}