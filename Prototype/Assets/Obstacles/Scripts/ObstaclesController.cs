using System;
using Assets.Planet.Scripts;
using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    public class ObstaclesController : MonoBehaviour
    {
        public RotationSpeedManager SpeedManager;

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.left, SpeedManager.Speed * Time.deltaTime, Space.Self);
        }
    }
}
