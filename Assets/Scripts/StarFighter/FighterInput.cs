using UnityEngine;

namespace Assets.Scripts.StarFighter
{
    public abstract class FighterInput : MonoBehaviour
    {
        public float Throttle { get; protected set; }
        public float Strafe { get; protected set; }
        public float Yaw { get; protected set; }
        public float Pitch { get; protected set; }
        public float Roll { get; protected set; }
    }
}
