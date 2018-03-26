﻿using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    public class DestroyObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (string.CompareOrdinal("Destroyer", other.gameObject.name) == 0) Destroy(gameObject);
        }
    }
}
