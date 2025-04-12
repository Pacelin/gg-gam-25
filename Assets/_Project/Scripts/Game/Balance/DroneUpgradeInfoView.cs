using TMPro;
using UnityEngine;

namespace GGJam25.Game.Balance
{
    public class DroneUpgradeInfoView : MonoBehaviour
    {
        [SerializeField] private string _valueFormat = "{0}";
        [SerializeField] private string _upgradeFormat = "{0}: {1} -> {2}";
        [SerializeField] private string _maxFormat = "{0}: {1}";
        [SerializeField] private TMP_Text _text;

        public void Set(string upgradeName, string firstValue, string secondValue)
        {
            _text.text = string.Format(_upgradeFormat, upgradeName,
                string.Format(_valueFormat, firstValue),
                string.Format(_valueFormat, secondValue));
        }
        
        public void Set(string upgradeName, string firstValue)
        {
            _text.text = string.Format(_maxFormat, upgradeName,
                string.Format(_valueFormat, firstValue));
        }
    }
}