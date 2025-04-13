using System.Collections.Generic;
using TSS.Tweening;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneShop : MonoBehaviour
    {
        [SerializeField] private ScriptableTween _showTween;
        [SerializeField] private ScriptableTween _hideTween;
        [SerializeField] private Transform _upgradesParent;
        [SerializeField] private DroneUpgradeView _upgradeViewPrefab;

        private List<DroneUpgradeView> _upgrades = new();

        public void Show()
        {
            if (_hideTween.IsPlaying)
                _hideTween.Kill();
            _showTween.Play();
        }

        public void Hide()
        {
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
        }

        private void OnDisable()
        {
            foreach (var upgrade in _upgrades)
                Destroy(upgrade.gameObject);
            _upgrades.Clear();
        }
    }
}