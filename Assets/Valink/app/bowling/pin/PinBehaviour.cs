using UnityEngine;

namespace Valink.app.bowling.pin
{
    public class PinBehaviour : MonoBehaviour
    {
        public bool isDropped;

        public delegate void PinDropped();
        public event PinDropped OnPinDropped;

        private void Update()
        {
            if (!isDropped && IsDropped())
            {
                isDropped = true;
                OnPinDropped?.Invoke();
            }
        }

        private bool IsDropped()
        {
            const int anglePrecision = 45;
            var localEulerAngles = transform.localEulerAngles;

            return (
                       !IsAngleAround(localEulerAngles.x, 0, anglePrecision) &&
                       !IsAngleAround(localEulerAngles.x, 360, anglePrecision)
                   ) ||
                   (
                       !IsAngleAround(localEulerAngles.z, 0, anglePrecision) &&
                       !IsAngleAround(localEulerAngles.z, 360, anglePrecision)
                   );
        }

        private bool IsAngleAround(float angle, float targetAngle, int anglePrecision)
        {
            return targetAngle - anglePrecision < angle && angle < targetAngle + anglePrecision;
        }
    }
}
