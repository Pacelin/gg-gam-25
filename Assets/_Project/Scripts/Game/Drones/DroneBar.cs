using R3;
using UnityEngine;

namespace GGJam25.Game.Indicators
{
    public abstract class DroneBar : MonoBehaviour
    {
        public abstract void Initialize(Observable<float> fill);
    }
}