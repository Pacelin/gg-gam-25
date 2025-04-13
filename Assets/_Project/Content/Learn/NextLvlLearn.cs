using GGJam25.Game;
using GGJam25.Game.Drones;
using UnityEngine;

public class NextLvlLearn : MonoBehaviour
{
    [Header("Switch Settings")]
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private bool destroyInsteadOfDisable = false;
    [SerializeField] private GameObject objectToDisable2;
    [SerializeField] private GameObject objectToEnable2;
    [SerializeField] private GameObject objectToEnable22;
    [Header("Trigger Settings")]
    [SerializeField] private GameObject requiredPrefab; 
    

    private void Update()
    {
        if (GameContext.DroneStorage.ResourcesInDrone.CurrentValue >= 50 && GameContext.CollectedKeys.Contains(2))
        {
            objectToEnable2.SetActive(true);
            objectToEnable22.SetActive(true);
            objectToDisable2.SetActive(false);
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (IsRequiredPrefab(other.gameObject))
        {
            SwitchObjects();
        }
    }

    bool IsRequiredPrefab(GameObject obj)
    {
        if (requiredPrefab == null) return false;
        return obj.name.StartsWith(requiredPrefab.name);
    }

    void SwitchObjects()
    {
        // Отключаем или уничтожаем объект
        if (objectToDisable != null)
        {
            if (destroyInsteadOfDisable)
                Destroy(objectToDisable);
            else
                objectToDisable.SetActive(false);
        }

        // Включаем новый объект
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}