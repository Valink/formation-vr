using UnityEngine;

namespace app.bowling
{
    public class GutterBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject ramp;
        
        public void EnableRamp()
        {
            ramp.SetActive(true);
        }
        
        public void DisableRamp()
        {
            ramp.SetActive(false);
        }
    }
}
