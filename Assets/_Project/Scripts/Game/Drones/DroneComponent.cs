using System;
using Cysharp.Threading.Tasks;
using GGJam25.Game.Indicators;
using R3;
using TSS.Core;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneComponent : MonoBehaviour
    {
        public DroneHealth Health => _health;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField] private ScriptableTween _deathTween;
        
        private bool _activeInput;
        private DroneHealth _health = new DroneHealth();

        private void OnEnable()
        {
            _health = new DroneHealth();
            _health.OnKill.Subscribe(_ =>
            {
                _deathTween.Play();
                _deathTween.WaitWhilePlay().ContinueWith(() => GameContext.DroneStation.Revive());
            });
        }

        private void OnDisable()
        {
            throw new NotImplementedException();
        }

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
                targetPosition = hit.point;
            else
                targetPosition = _rigidbody.position;
            if (targetPosition == _rigidbody.position)
                return;
            
            var targetRotation = Quaternion.LookRotation(targetPosition - _rigidbody.position);
            var pos = Vector3.MoveTowards(_rigidbody.position, targetPosition, 
                GameContext.DroneUpgrades.LinearSpeed.CurrentValue * Time.fixedDeltaTime);
            var rot = Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, 
                GameContext.DroneUpgrades.AngularSpeed.CurrentValue * Time.fixedDeltaTime);
            _rigidbody.Move(pos, rot);
        }
    }
}