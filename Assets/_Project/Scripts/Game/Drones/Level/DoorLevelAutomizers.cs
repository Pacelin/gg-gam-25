#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DoorLevelAutomizers : MonoBehaviour
    {
        [ContextMenu("Automate")]
        private void Automate()
        {
            var doors = FindObjectsByType<DoorComponent>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID)
                .ToList();
            while (doors.Count > 0)
            {
                var door = doors.First();
                var neighbour = FindNeighbour(doors, door);
                var serializedFirst = new SerializedObject(door);
                var serializedSecond = new SerializedObject(neighbour);
                serializedFirst.FindProperty("_neighbour").objectReferenceValue = neighbour;
                serializedSecond.FindProperty("_neighbour").objectReferenceValue = door;
                doors.RemoveAt(0);
                serializedFirst.ApplyModifiedProperties();
                serializedSecond.ApplyModifiedProperties();
                doors.Remove(neighbour);
            }
            EditorUtility.SetDirty(this);
        }

        private DoorComponent FindNeighbour(List<DoorComponent> doors, DoorComponent door)
        {
            DoorComponent find = null;
            float distance = float.MaxValue;
            foreach (var neigh in doors)
            {
                if (door.Octagon == neigh.Octagon)
                    continue;
                var doorDirection = -door.transform.forward;
                var targetDirection = (neigh.transform.position - door.transform.position).normalized;
                if (Vector3.Angle(doorDirection, targetDirection) > 5)
                    continue;
                var curDistance = Vector3.Distance(door.transform.position, neigh.transform.position);
                if (curDistance < distance)
                {
                    distance = curDistance;
                    find = neigh;
                }
            }

            return find;
        }
    }
}
#endif