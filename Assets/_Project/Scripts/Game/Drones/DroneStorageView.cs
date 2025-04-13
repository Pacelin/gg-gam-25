using R3;
using TMPro;
using UnityEngine;

namespace GGJam25.Game.Drones
{
    public class DroneStorageView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _resourcesText;
        [SerializeField] private string _resourcesInDroneFormat = "{0}/{1}";
        [SerializeField] private TMP_Text _resourcesInDroneText;

        private CompositeDisposable _disposables;
        
        private void OnEnable()
        {
            _disposables = new();
            GameContext.DroneStorage.Resources.Subscribe(r =>
                    _resourcesText.text = r.ToString())
                .AddTo(_disposables);
            GameContext.DroneStorage.ResourcesInDrone.CombineLatest(
                    GameContext.DroneUpgrades.StorageCapacity, (inDrone, capacity) => (inDrone, capacity))
                .Subscribe(pair =>
                    _resourcesInDroneText.text = string.Format(_resourcesInDroneFormat, pair.inDrone, pair.capacity))
                .AddTo(_disposables);
        }

        private void OnDisable()
        {
            _disposables?.Dispose();
        }
    }
}