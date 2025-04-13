using GGJam25.Game;
using GGJam25.Game.Indicators;
using UnityEngine;

public class NextLvlLearn : MonoBehaviour
{
    [Header("Switch Settings")]
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private GameObject objectToEnable2;
    [SerializeField] private GameObject objectToEnable22;
    

    private void Update()
    {
        if (GameContext.DroneStorage.ResourcesInDrone.CurrentValue >= 50 && GameContext.CollectedKeys.Contains(2))
        {
            objectToEnable2.SetActive(true);
            objectToEnable22.SetActive(true);
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
        // �������� ����� ������
        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true);
        }
    }
}