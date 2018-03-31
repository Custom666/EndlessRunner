using System;
using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    public class ObstaclesController : MonoBehaviour
    {
        public float Speed = 20f;
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Mathf.Abs(Time.fixedTime % 15f) < 0.00001f) Speed += 10f;

            transform.Rotate(Vector3.left, Speed * Time.deltaTime, Space.Self);
        }
    }
}
