using UnityEngine;

namespace app.bowling.pin
{
    public class PinBehaviour : MonoBehaviour
    {
        public bool isDropped;

        private void Update()
        {
            if (!isDropped && IsDropped())
            {
                isDropped = true;
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
