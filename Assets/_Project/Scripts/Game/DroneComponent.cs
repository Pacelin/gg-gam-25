using TSS.Core;
using UnityEngine;

namespace LudumDare57.Game
{
    [System.Serializable]
    public struct SpeedLevel
    {
        public float LinearSpeed;
        public float AngularSpeed;
    }
    
    public class DroneComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Vector3 _flyOffset;
        [Space] 
        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField] private SpeedLevel[] _speedLevels;

        private bool _activeInput;

        private void Update()
        {
            if (Runtime.IsPaused)
                return;
            _activeInput = Input.GetKey(KeyCode.Mouse0);
        }

        private void FixedUpdate()
        {
            if (Runtime.IsPaused)
                return;
            if (!_activeInput)
                return;

            Vector3 targetPosition;
            var ray = SceneCameraProvider.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, float.MaxValue, _floorLayerMask))
                targetPosition = hit.point + _flyOffset;
            else
                targetPosition = _rigidbody.position;
            if (targetPosition == _rigidbody.position)
                return;
            
            var targetRotation = Quaternion.LookRotation(targetPosition - _rigidbody.position);
            
            var speedLevel = _speedLevels[GameContext.DroneSpeedLevel];
            var pos = Vector3.MoveTowards(_rigidbody.position, targetPosition, speedLevel.LinearSpeed * Time.fixedDeltaTime);
            var rot = Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, speedLevel.AngularSpeed * Time.fixedDeltaTime);
            _rigidbody.Move(pos, rot);
        }
    }
}