using System;
using Cysharp.Threading.Tasks;
using R3;
using TSS.ContentManagement;
using TSS.Core;
using TSS.SceneManagement;
using UnityEngine;

namespace LudumDare57.UI
{
    [RequireComponent(typeof(ScriptableButton))]
    public class PlayButton : MonoBehaviour
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
                SceneManager.Scene(CMS.Scenes.Game).Single().Load(Runtime.CancellationToken).Forget());
        }

        private void OnDisable()
        {
            _disposable.Dispose();
        }
    }
}