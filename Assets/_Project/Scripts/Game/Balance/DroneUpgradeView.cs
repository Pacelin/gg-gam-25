using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJam25.Game.Balance
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
        [SerializeField] private GameObject _levelState;
        [SerializeField] private GameObject _levelMaxState;
        [SerializeField] private RectTransform _infoParent;
        [SerializeField] private DroneUpgradeInfoView _infoPrefab;

        private readonly List<DroneUpgradeInfoView> _infos = new List<DroneUpgradeInfoView>();

        public void SetName(string upgradeName) => _nameText.text = string.Format(_nameFormat, upgradeName);
        public void SetDescription(string upgradeDescription) => _descriptionText.text = string.Format(_descriptionFormat, upgradeDescription);
        public void SetIcon(Sprite upgradeIcon) => _iconImage.sprite = upgradeIcon;
        public void SetPrice(int price) => _priceText.text = string.Format(_priceFormat, price);

        public void SetMax(bool max)
        {
            _levelState.SetActive(!max);
            _levelMaxState.SetActive(max);
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

        public void ClearInfo()
        {
            foreach (var info in _infos)
                Destroy(info.gameObject);
            _infos.Clear();
        }
    }
}