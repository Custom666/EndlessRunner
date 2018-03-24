using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class ObstaclesController : MonoBehaviour
    {
        public float SpawnInterval = 5f;

        public Vector3 SpawnOffset = new Vector3(0, 0, 15);

        public GameObject Obstacle;

        private float _deltaTime;

        private void Start()
        {
            _deltaTime = SpawnInterval;
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.left);

            _deltaTime -= Time.fixedDeltaTime;

            if (!(_deltaTime <= 0f)) return;

            var obstacle = Instantiate(Obstacle, transform.position + SpawnOffset, transform.localRotation);

            obstacle.transform.parent = transform;

            _deltaTime = SpawnInterval;
        }
    }
}
