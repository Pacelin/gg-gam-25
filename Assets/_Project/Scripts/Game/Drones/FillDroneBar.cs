using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace GGJam25.Game.Indicators
{
    public class FillDroneBar : MonoBehaviour
    {
        [SerializeField] private Image _fillNegativeImage;
        [SerializeField] private Image _fillPositiveImage;
        [SerializeField] private float _multiplier = 1;
        
        private IDisposable _dipsosable;
        
        public void Initialize(Observable<float> fill)
        {
            _dipsosable?.Dispose();
            _dipsosable = fill.Subscribe(f =>
            {
                float negative = 0;
                float positive = 0;
                if (f > 0)
                    positive = f;
                else
                    negative = -f;
                _fillNegativeImage.fillAmount = negative * _multiplier;
                _fillPositiveImage.fillAmount = positive * _multiplier;
            });
        }

        private void OnDestroy() => _dipsosable?.Dispose();
    }
}