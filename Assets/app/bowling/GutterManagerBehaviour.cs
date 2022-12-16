using app.bowling.room;
using UnityEngine;

namespace app.bowling
{
    class GutterManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private LaneSizer laneSizer;
        [SerializeField] private GutterBehaviour[] gutters;
        [SerializeField] private bool isEnabled;

        public void Start()
        {
            gutters = new[] { laneSizer.leftGutter, laneSizer.rightGutter };
        }

        public void ToggleRamps()
        {
            if (isEnabled)
            {
                isEnabled = false;
                foreach (var gutter in gutters)
                {
                    gutter.gameObject.SetActive(false);
                }
            }
            else
            {
                isEnabled = true;
                foreach (var gutter in gutters)
                {
                    gutter.gameObject.SetActive(true);
                }
            }
        }
    }
}