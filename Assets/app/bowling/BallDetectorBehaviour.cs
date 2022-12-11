using UnityEngine;

namespace app.bowling
{
    public class BallDetectorBehaviour : MonoBehaviour
    {
        public delegate void BallEnterEvent();
        public event BallEnterEvent BallEntered;
    
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                BallEntered?.Invoke();            
            }
        }
    }
}
