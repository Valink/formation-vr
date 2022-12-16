using UnityEngine;

namespace app.bowling
{
    class GutterManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private GutterBehaviour[] gutters;
        [SerializeField] private bool isEnabled;

        public void ToggleRamps()
        {
            if (isEnabled)
            {
                isEnabled = false;
                foreach (var gutter in gutters)
                {
                    gutter.DisableRamp();
                }
            }
            else
            {
                isEnabled = true;
                foreach (var gutter in gutters)
                {
                    gutter.EnableRamp();
                }
            }
        }
    }
}