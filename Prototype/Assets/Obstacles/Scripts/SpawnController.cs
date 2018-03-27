using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    public class SpawnController : MonoBehaviour
    {
        public float SpawnInterval = 5f;

        public GameObject Obstacle;

        public Transform Origin;

        private float _deltaTime;

        private void Start()
        {
            _deltaTime = SpawnInterval;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            _deltaTime -= Time.fixedDeltaTime;

            if (!(_deltaTime <= 0f)) return;

            Instantiate(Obstacle, transform.position, Obstacle.transform.localRotation, Origin);
            
            _deltaTime = SpawnInterval;
        }
    }
}