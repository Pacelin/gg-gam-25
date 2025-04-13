using System.Collections.Generic;
using GGJam25.UI;
using R3;
using TMPro;
using TSS.Audio;
using UnityEngine;
using UnityEngine.UI;

namespace GGJam25.Game.Drones
{
    public class DroneUpgradeView : MonoBehaviour
    {
        [SerializeField] private string _nameFormat = "{0}";
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private string _descriptionFormat = "{0}";
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private string _priceFormat = "{0}";
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Image _iconImage;
        [SerializeField] private GameObject[] _levelState;
        [SerializeField] private GameObject[] _levelMaxState;
        [SerializeField] private RectTransform _infoParent;
        [SerializeField] private DroneUpgradeInfoView _infoPrefab;
        [SerializeField] private ScriptableButton _buyButton;
        
        private CompositeDisposable _disposables;
        private List<DroneUpgradeInfoView> _infos = new();

        public void Init(ReactiveProperty<int> level, DroneUpgrade upgrade)
        {
            _disposables = new();
            level.Subscribe(l =>
            {
                foreach(var info in _infos)
                    Destroy(info.gameObject);
                _infos.Clear();
                upgrade.OnView(l, this);
            }).AddTo(_disposables);
            level.CombineLatest(GameContext.DroneStorage.Resources, (l, r) => (l, r))
                .Subscribe(p =>
                {
                    if (upgrade.IsMax(p.l))
                        return;
                    var price = upgrade.GetPrice(p.l);
                    _buyButton.Interactable = p.r >= price;
                }).AddTo(_disposables);
            _buyButton.ObserveFeedbackStart().Subscribe(_ =>
            {
                GameContext.DroneStorage.Buy(upgrade.GetPrice(level.Value));
                level.Value++;
                AudioSystem.Game_BuyUpgrade.PlayOneShot();
            }).AddTo(_disposables);
        }
        
        private void OnDisable()
        {
            _disposables?.Dispose();
        }

        public void SetName(string upgradeName, int level)
        {
            var posfix = level == 0 ? "" :
                level == 1 ? " I уровня" :
                level == 2 ? " II уровня" :
                " III уровня";
            _nameText.text = string.Format(_nameFormat, upgradeName) + posfix;
        } 
        public void SetDescription(string upgradeDescription) => _descriptionText.text = string.Format(_descriptionFormat, upgradeDescription);
        public void SetIcon(Sprite upgradeIcon) => _iconImage.sprite = upgradeIcon;
        public void SetPrice(int price) => _priceText.text = string.Format(_priceFormat, price);

        public void SetMax(bool max)
        {
            foreach (var ls in _levelState)
                ls.SetActive(!max);
            foreach (var ls in _levelMaxState)
                ls.SetActive(max);
        }

        public void AddInfo(string valueName, string curValue)
        {
            var newInfo = Instantiate(_infoPrefab, _infoParent);
            newInfo.Set(valueName, curValue);
            _infos.Add(newInfo);
        }

        public void AddInfo(string valueName, string curValue, string nextValue)
        {
            var newInfo = Instantiate(_infoPrefab, _infoParent);
            newInfo.Set(valueName, curValue, nextValue);
            _infos.Add(newInfo);
        }
    }
}