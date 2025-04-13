using System;
using TMPro;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneUpgradeInfoView : MonoBehaviour
    {
        [SerializeField] private string _valueFormat = "{0}";
        [SerializeField] private string _upgradeFormat = "{0}: {1} -> {2}";
        [SerializeField] private string _captionFormat = "{0}:";
        [SerializeField] private string _maxFormat = "{0}: {1}";
        [SerializeField] private TMP_Text _text;
        [SerializeField] private TMP_Text _captionText;

        public void Set(string upgradeName, string firstValue, string secondValue)
        {
            _captionText.text = String.Format(_captionFormat, upgradeName);
            _text.text = string.Format(_upgradeFormat,
                string.Format(_valueFormat, firstValue),
                string.Format(_valueFormat, secondValue));
        }
        
        public void Set(string upgradeName, string firstValue)
        {
            _captionText.text = String.Format(_captionFormat, upgradeName);
            _text.text = string.Format(_maxFormat, 
                string.Format(_valueFormat, firstValue));
        }
    }
}