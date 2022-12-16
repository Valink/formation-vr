using UnityEngine;

namespace app.bowling
{
    public class BallSpawnerBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject balls;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform spawnPoint;

        public GameObject SpawnBall()
        {
            var ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ball.transform.parent = balls.transform;
            return ball;
        }
        
        public void DestroyAllBalls()
        {
            foreach (Transform child in balls.transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}