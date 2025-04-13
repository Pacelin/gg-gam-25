using UnityEditor;
using UnityEngine;

namespace GGJam25.Editor
{
    public class SwitchToWall
    {
        private static string WallPath
        {
            get => EditorPrefs.GetString("wall_path", null);
            set => EditorPrefs.SetString("wall_path", value);
        }
        
        [MenuItem("Assets/Select As Wall")]
        public static void SelectAsset()
        {
            WallPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            Debug.Log(WallPath);
        }
        
        [MenuItem("Tools/Switch Selected To Wall")]
        public static void SwitchToWallMethod()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(WallPath);
            foreach (var go in Selection.gameObjects)
            {
                var transform = go.transform;
                var parent = transform.parent;
                var pos = transform.position;
                var rotation = transform.rotation;
                var prefabInstance = PrefabUtility.InstantiatePrefab(prefab, parent) as GameObject;
                prefabInstance.name = "Wall";
                prefabInstance.transform.position = pos;
                prefabInstance.transform.rotation = rotation;
                EditorUtility.SetDirty(parent);
                Object.DestroyImmediate(go);
            }
        }
    }
}