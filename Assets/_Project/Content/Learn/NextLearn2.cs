using GGJam25.Game;
using GGJam25.Game.Indicators;
using UnityEngine;

public class NextLearn2 : MonoBehaviour
{
    [Header("Switch Settings")]
    [SerializeField] private GameObject objectToDisable;
    [SerializeField] private GameObject objectToEnable;
    [SerializeField] private GameObject objectToEnable0;
    [SerializeField] private bool destroyInsteadOfDisable = false;
    [SerializeField] private GameObject objectToDisable2;
    [SerializeField] private GameObject objectToDisable22;
    [SerializeField] private GameObject objectToEnable2;
    [Header("Trigger Settings")]
    [SerializeField] private GameObject requiredPrefab;


    private void Update()
    {
        if (GameContext.DroneUpgrades.RadarLevel.CurrentValue == 1 && GameContext.CollectedKeys.Count == 1)
        {
            /*objectToEnable2.SetActive(true);
            objectToDisable2.SetActive(false);*/
            Destroy(objectToDisable22);
            Destroy(objectToDisable2);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<DroneTriggerPoint>(out _))
            SwitchObjects();
    }

    bool IsRequiredPrefab(GameObject obj)
    {
        if (requiredPrefab == null) return false;
        return obj.name.StartsWith(requiredPrefab.name);
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
            objectToEnable0.SetActive(true);
        }
    }
}