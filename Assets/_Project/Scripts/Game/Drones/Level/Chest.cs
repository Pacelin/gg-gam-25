using System;
using Cysharp.Threading.Tasks;
using TSS.ContentManagement;
using TSS.Core;
using TSS.SceneManagement;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class Chest : MonoBehaviour
    {
        [SerializeField] private GameObject[] _keys;
        [SerializeField] private ScriptableTween _endingTween;

        private bool _ending;
        private void Update()
        {
            if (_ending)
                return;
            for (int i = 0; i < _keys.Length; i++)
                _keys[i].gameObject.SetActive(GameContext.CollectedKeys.Contains(i));
            if (GameContext.CollectedKeys.Count == _keys.Length &&
                GameContext.OctagonController.ActiveOctagon == GameContext.Level.Hub.transform)
            {
                _ending = true;
                _endingTween.Play();
                _endingTween.WaitWhilePlay().ContinueWith(async () =>
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(2));
                    await SceneManager.Scene(CMS.Scenes.MainMenu).Load(Runtime.CancellationToken);
                });
            }
        }
    }
}