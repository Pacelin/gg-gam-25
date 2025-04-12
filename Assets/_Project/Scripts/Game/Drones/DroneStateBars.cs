using R3;
using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public class DroneStateBars : MonoBehaviour
    {
        [SerializeField] private DroneBar _waterBar;
        [SerializeField] private DroneBar _tempBar;

        private void OnEnable()
        {
            _waterBar.Initialize(
                GameContext.DroneStation.ActiveDrone
                    .SelectMany(drone =>
                    {
                        if (drone)
                            return drone.Health.Water;
                        return Observable.Return<float>(0);
                    }));
            _tempBar.Initialize(
                GameContext.DroneStation.ActiveDrone
                    .SelectMany(drone =>
                    {
                        if (drone)
                            return drone.Health.Temperature;
                        return Observable.Return<float>(0);
                    }));
        }
    }
}