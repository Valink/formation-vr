using UnityEngine;

namespace Valink.app.bowling
{
    public class BallTrigger : MonoBehaviour
    {
        public delegate void BallEnterEvent();
        public event BallEnterEvent OnBallReachLaneEnd;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Ball"))
            {
                OnBallReachLaneEnd?.Invoke();            
            }
        }
    }
}
