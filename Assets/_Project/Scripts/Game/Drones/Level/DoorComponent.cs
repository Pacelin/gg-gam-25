using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DoorComponent : MonoBehaviour
    {
        public Transform Octagon => _octagon;
        public Transform Entry => _entry;
        public DoorComponent Neighbour => _neighbour;
        
        [SerializeField] private Transform _octagon;
        [SerializeField] private Transform _entry;
        [SerializeField] private DoorComponent _neighbour;
        [SerializeField] private ScriptableTween _openTween;
        [SerializeField] private ScriptableTween _closeTween;

        public void Open()
        {
            if (_closeTween.IsPlaying)
                _closeTween.Kill();
            _openTween.Play();
        }

        public void Close()
        {
            if (_openTween.IsPlaying)
                _openTween.Kill();
            _closeTween.Play();
        }
    }
}