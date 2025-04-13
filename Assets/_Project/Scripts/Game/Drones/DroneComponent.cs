using System;
using Cysharp.Threading.Tasks;
using GGJam25.Game.Indicators;
using R3;
using TSS.Core;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GGJam25.Game.Drones
{
    public class DroneComponent : MonoBehaviour
    {
        public DroneHealth Health => _health;
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private LayerMask _floorLayerMask;
        [SerializeField] private float _stopDistance = 1;
        [SerializeField] private ScriptableTween _deathTween;

        private bool _locked;
        private bool _activeInput;
        private IDisposable _disposable;
        private DroneHealth _health = new DroneHealth();

        public void Lock()
        {
            _locked = true;
            _collider.enabled = false;
            _rigidbody.isKinematic = true;
        }

        public void Unlock()
        {
            _locked = false;
            _collider.enabled = true;
            _rigidbody.isKinematic = false;
        }

        private void OnEnable()
        {
            _health = new DroneHealth();
            _disposable = _health.OnKill.Subscribe(_ =>
            {
                GameContext.Level.Hub.Station.Death();
                if (_deathTween)
                {
                    _deathTween.Play();
                    _deathTween.WaitWhilePlay().ContinueWith(() => GameContext.Level.Hub.Station.Revive());
                }
                else
                {
                    GameContext.Level.Hub.Station.Revive();
                }
            });
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }

        private void Update()
        {
            if (Runtime.IsPaused)
                return;
            _activeInput = Input.GetKey(KeyCode.Mouse0) && !EventSystem.current.IsPointerOverGameObject();
        }

        private void FixedUpdate()
        {
            if (_locked)
                return;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
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
            var rot = Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, 
                GameContext.DroneUpgrades.AngularSpeed.CurrentValue * Time.fixedDeltaTime);
            if (Vector3.Distance(targetPosition, _rigidbody.position) < _stopDistance)
            {
                _rigidbody.MoveRotation(rot);
            }
            else
            {
                var pos = Vector3.MoveTowards(_rigidbody.position, targetPosition, 
                    GameContext.DroneUpgrades.LinearSpeed.CurrentValue * Time.fixedDeltaTime);
                _rigidbody.Move(pos, rot);
            }
        }
    }
}