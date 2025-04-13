using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using R3;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GGJam25.UI
{
    [PublicAPI]
    [RequireComponent(typeof(Image))]
    public class ScriptableButton : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
    {
        private enum EState
        {
            Unset,
            Disabled,
            Default,
            Hover,
            Down,
        }

        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                    return;
                _interactable = value;
                if (!_interactable)
                {
                    if (_down)
                    {
                        _onUp.OnNext(Unit.Default);
                        _down = false;
                    }
                    if (_hover)
                    {
                        _onExit.OnNext(Unit.Default);
                        _hover = false;
                    }
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
        [Space] 
        [SerializeField] private UnityEvent _onEnterEvent = new();
        [SerializeField] private UnityEvent _onDownEvent = new();
        [SerializeField] private UnityEvent _onClickEvent = new();
        
        private bool _down;
        private bool _hover;
        private EState _activeState = EState.Unset;
        
        private readonly Subject<Unit> _onDown = new();
        private readonly Subject<Unit> _onUp = new();
        private readonly Subject<Unit> _onEnter = new();
        private readonly Subject<Unit> _onExit = new();
        private readonly Subject<Unit> _onAfterFeedback = new();
        private readonly Subject<Unit> _onBeforeFeedback = new();

        public Observable<Unit> ObserveFeedbackStart() => _onBeforeFeedback;
        public Observable<Unit> ObserveFeedbackEnd() => _onAfterFeedback;

        private void OnValidate()
        {
            var img = GetComponent<Image>();
            img.color = Color.clear;
            img.raycastTarget = true;
            if (_interactable)
            {
                _toDefaultTween.Play();
                _toDefaultTween.Complete(true);
                _toDefaultTween.Kill();
            }
            else
            {
                _toDisabledTween.Play();
                _toDisabledTween.Complete(true);
                _toDisabledTween.Kill();
            }
        }
        
        private void Awake()
        {
            if (_interactable)
            {
                _toDefaultTween.Play();
                _toDefaultTween.Complete(true);
                _activeState = EState.Default;
            }
            else
            {
                _toDisabledTween.Play();
                _toDisabledTween.Complete(true);
                _activeState = EState.Disabled;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            _hover = true;
            _onEnter.OnNext(Unit.Default);
            _onEnterEvent.Invoke();
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
            _onDownEvent.Invoke();
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
            var currentState = EState.Default;
            var tween = _toDefaultTween;

            if (!_interactable)
            {
                currentState = EState.Disabled;
                tween = _toDisabledTween;
            }
            else if (_down)
            {
                currentState = EState.Down;
                tween = _toDownTween;
            }
            else if (_hover)
            {
                currentState = EState.Hover;
                tween = _toHoverTween;
            }

            if (currentState != _activeState)
            {
                _activeState = currentState;
                tween.Play();
                if (!gameObject.activeInHierarchy)
                    tween.Complete(true);
            }
        }

        private void KillTransitionTweens()
        {
            if (_toDisabledTween.IsPlaying)
                _toDisabledTween.Kill();
            if (_toDefaultTween.IsPlaying)
                _toDefaultTween.Kill();
            if (_toDownTween.IsPlaying)
                _toDownTween.Kill();
            if (_toHoverTween.IsPlaying)
                _toHoverTween.Kill();
        }
    }
}