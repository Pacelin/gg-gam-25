using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public class DroneRadar : MonoBehaviour
    {
        [SerializeField] private GameObject _enabledState;
        [SerializeField] private GameObject _disabledState;
        [SerializeField] private Transform _arrowHub;
        [SerializeField] private Transform _arrowKey;

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}