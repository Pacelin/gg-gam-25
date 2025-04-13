using GGJam25.Game;
using GGJam25.Game.Drones;
using GGJam25.Game.Indicators;
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
        if (other.TryGetComponent<DroneTriggerPoint>(out _))
            SwitchObjects();
    }


    void SwitchObjects()
    {
        // ��������� ��� ���������� ������
        if (objectToDisable != null)
        {
            if (destroyInsteadOfDisable)
                Destroy(objectToDisable);
            else
                objectToDisable.SetActive(false);
        }

        // �������� ����� ������
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}