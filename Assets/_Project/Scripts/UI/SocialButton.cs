using System;
using R3;
using UnityEngine;

namespace GGJam25.UI
{
    [RequireComponent(typeof(ScriptableButton))]
    public class SocialButton : MonoBehaviour
    {
        [SerializeField] private ScriptableButton _scriptableButton;
        [SerializeField] private string _url;
        
        private IDisposable _disposable;

        private void OnValidate()
        {
            if (!_scriptableButton)
                _scriptableButton = GetComponent<ScriptableButton>();
        }

        private void OnEnable()
        {
            _disposable = _scriptableButton.ObserveFeedbackEnd()
                .Subscribe(_ => Application.OpenURL(_url));
        }

        private void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}