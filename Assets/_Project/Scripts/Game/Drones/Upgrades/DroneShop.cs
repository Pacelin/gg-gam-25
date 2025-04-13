using System.Collections.Generic;
using TSS.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GGJam25.Game.Drones
{
    public class DroneShop : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private ScriptableTween _showTween;
        [SerializeField] private ScriptableTween _hideTween;
        [SerializeField] private Transform _upgradesParent;
        [SerializeField] private DroneUpgradeView _upgradeViewPrefab;

        private bool _opened = false;
        private List<DroneUpgradeView> _upgrades = new();

        public void Switch()
        {
            if (_opened)
                Hide();
            else
                Show();
        }
        
        public void Show()
        {
            _opened = true;
            if (_hideTween.IsPlaying)
                _hideTween.Kill();
            _showTween.Play();
        }

        public void Hide()
        {
            _opened = false;
            if (_showTween.IsPlaying)
                _showTween.Kill();
            _hideTween.Play();
        }
        
        private void OnEnable()
        {
            foreach (var upgrade in GameContext.DroneUpgrades.AllUpgrades())
            {
                var view = Instantiate(_upgradeViewPrefab, _upgradesParent);
                view.Init(upgrade.Level, upgrade.Upgrade);
                _upgrades.Add(view);
            }
            _closeButton.onClick.AddListener(OnCloseClick);
        }

        private void OnDisable()
        {
            foreach (var upgrade in _upgrades)
                Destroy(upgrade.gameObject);
            _upgrades.Clear();
            _closeButton.onClick.RemoveListener(OnCloseClick);
        }

        private void OnCloseClick()
        {
            if (_opened)
                Hide();
        }
    }
}