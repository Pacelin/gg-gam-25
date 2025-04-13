using GGJam25.Game.Drones;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GGJam25.Game.Editor
{
    public class ResourceInstantiator : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField] private float _radius;
        [SerializeField] private ResourceSpawner _spawnerPrefab;

        #if UNITY_EDITOR
        [ContextMenu("Generate")]
        private void Generate()
        {
            while (transform.childCount > 0)
                DestroyImmediate(transform.GetChild(0).gameObject);
            for (int i = 0; i < _count; i++)
            {
                var prefab = PrefabUtility.InstantiatePrefab(_spawnerPrefab) as ResourceSpawner;
                var r = Random.insideUnitCircle * _radius;
                prefab.transform.SetParent(transform);
                prefab.transform.localPosition = new Vector3(r.x, 0, r.y);
            }
            EditorUtility.SetDirty(gameObject);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
#endif
    }
}