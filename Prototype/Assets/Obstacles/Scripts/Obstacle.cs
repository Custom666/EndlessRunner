using System;
using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.name)
            {
                case "Destroyer":
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
