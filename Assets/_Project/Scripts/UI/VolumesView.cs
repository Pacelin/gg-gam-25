using System;
using R3;
using TSS.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace LudumDare57.UI
{
    public class VolumesView : MonoBehaviour
    {
        [SerializeField] private ScriptableSlider _master;
        [SerializeField] private ScriptableSlider _music;
        [SerializeField] private ScriptableSlider _sfx;

        private IDisposable _disposable;
        
        private void OnEnable()
        {
            _master.Value = (AudioSystem.Volumes.MasterVolume);
            _music.Value = (AudioSystem.Volumes.GetVolume(0));
            _sfx.Value = (AudioSystem.Volumes.GetVolume(1));
            _disposable = new CompositeDisposable()
            {
                _master.OnValueChanged.Subscribe(v => AudioSystem.Volumes.MasterVolume = v),
                _music.OnValueChanged.Subscribe(v => AudioSystem.Volumes.SetVolume(0, v)),
                _sfx.OnValueChanged.Subscribe(v => AudioSystem.Volumes.SetVolume(0, v))
            };
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}