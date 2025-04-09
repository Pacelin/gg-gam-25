using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LudumDare57.UI
{
    [PublicAPI]
    [RequireComponent(typeof(Image))]
    public class FeedbackButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        public bool Hold => _down;
        public bool Hover => _hover;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                    return;
                _interactable = value;
                if (_hover)
                {
                    _onExit.OnNext(Unit.Default);
                    _hover = false;
                }
                if (_down)
                {
                    _onUp.OnNext(Unit.Default);
                    _down = false;
                }
                UpdateState();
            }
        }
        
        [SerializeField] private bool _interactable = true;
        [SerializeField] private ScriptableTween _toDefaultTween;
        [SerializeField] private ScriptableTween _toHoverTween;
        [SerializeField] private ScriptableTween _toDownTween;
        [SerializeField] private ScriptableTween _toDisabledTween;
        [SerializeField] private ScriptableTween _feedbackTween;
        [SerializeField] private bool _lockInputOnFeedback;

        private bool _down;
        private bool _hover;
        private readonly Subject<Unit> _onDown = new();
        private readonly Subject<Unit> _onUp = new();
        private readonly Subject<Unit> _onEnter = new();
        private readonly Subject<Unit> _onExit = new();
        private readonly Subject<Unit> _onAfterFeedback = new();
        private readonly Subject<Unit> _onBeforeFeedback = new();

        public Observable<Unit> ObserveFeedbackStart() => _onBeforeFeedback;
        public Observable<Unit> ObserveFeedbackEnd() => _onAfterFeedback;

        private void Awake()
        {
            if (_interactable)
            {
                _toDefaultTween.Play();
                _toDefaultTween.Complete(true);
            }
            else
            {
                _toDisabledTween.Play();
                _toDisabledTween.Complete(true);
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            _hover = true;
            _onEnter.OnNext(Unit.Default);
            UpdateState();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            _hover = false;
            _onExit.OnNext(Unit.Default);
            if (_down)
                _onUp.OnNext(Unit.Default);
            _down = false;
            UpdateState();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            _down = true;
            _onDown.OnNext(Unit.Default);
            UpdateState();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            if (!_down)
                return;
            _down = false;
            _onUp.OnNext(Unit.Default);
            _onBeforeFeedback.OnNext(Unit.Default);
            if (_feedbackTween)
            {
                KillTransitionTweens();
                _feedbackTween.Play();
                if (_lockInputOnFeedback)
                    OverlayInputLock.Enable();
                _feedbackTween.WaitWhilePlay().ContinueWith(() =>
                {
                    if (_lockInputOnFeedback)
                        OverlayInputLock.Disable();
                    UpdateState();
                    _onAfterFeedback.OnNext(Unit.Default);
                });
            }
            else
            {
                UpdateState();
                _onAfterFeedback.OnNext(Unit.Default);
            }
        }

        private void UpdateState()
        {
            if (_feedbackTween && _feedbackTween.IsPlaying)
                return;
            KillTransitionTweens();
            var tween = _toDefaultTween;
            if (!_interactable)
                tween = _toDisabledTween;
            else if (_down)
                tween = _toDownTween;
            else if (_hover)
                tween = _toHoverTween;
            tween.Play();
            if (!gameObject.activeInHierarchy)
                tween.Complete(true);
        }

        private void KillTransitionTweens()
        {
            if (_toDisabledTween.IsPlaying)
                _toDisabledTween.Pause();
            if (_toDefaultTween.IsPlaying)
                _toDefaultTween.Pause();
            if (_toDownTween.IsPlaying)
                _toDownTween.Pause();
            if (_toHoverTween.IsPlaying)
                _toHoverTween.Pause();
        }
    }
}