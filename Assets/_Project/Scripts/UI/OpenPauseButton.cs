using System;
using R3;
using UnityEngine;

namespace LudumDare57.UI
{
    [RequireComponent(typeof(ScriptableButton))]
    public class OpenPauseButton : MonoBehaviour
    {
        [SerializeField] private ScriptableButton _scriptableButton;

        private IDisposable _disposable;

        private void OnValidate()
        {
            if (!_scriptableButton)
                _scriptableButton = GetComponent<ScriptableButton>();
        }

        private void OnEnable()
        {
            _disposable = _scriptableButton.ObserveFeedbackStart().Subscribe(_ =>
                PauseMenu.Open());
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}