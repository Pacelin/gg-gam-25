using System;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private GameObject[] _keys;

        private void Update()
        {
            for (int i = 0; i < _keys.Length; i++)
                _keys[i].gameObject.SetActive(GameContext.CollectedKeys.Contains(i));
        }
    }
    
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