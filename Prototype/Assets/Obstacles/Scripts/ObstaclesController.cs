using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    public class ObstaclesController : MonoBehaviour
    {
        public float Speed = 5f;
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.left, Speed * Time.deltaTime, Space.Self);
        }
    }
}
