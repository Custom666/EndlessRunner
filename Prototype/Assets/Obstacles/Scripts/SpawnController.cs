using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Obstacles.Scripts
{
    [Serializable]
    public class SpawnController : MonoBehaviour
    {
        public float SpawnInterval = 3f;

        public float Deflection = 0f;

        [SerializeField]
        public List<GameObject> Obstacles;

        public Transform Origin;

        public void Spawn(GameObject obstacle)
        {
            var newObstacle = Instantiate(obstacle, transform);

            newObstacle.transform.localEulerAngles += Vector3.forward * Deflection;

            newObstacle.transform.parent = Origin;
        }
    }
}