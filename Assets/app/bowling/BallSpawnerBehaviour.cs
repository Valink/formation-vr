using UnityEngine;

namespace app.bowling
{
    public class BallSpawnerBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private Transform spawnPoint;

        public GameObject SpawnBall()
        {
           return Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
        }        
    }
}
