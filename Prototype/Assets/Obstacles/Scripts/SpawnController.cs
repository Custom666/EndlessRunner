using System.Collections.Generic;
using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    /// <summary>
    /// Controller that handle obstacles collection and spawn one specific under given origin. 
    /// </summary>
    public class SpawnController : MonoBehaviour
    {
        public float SpawnInterval = 3f;

        public List<GameObject> Obstacles;

        [SerializeField]
        private float Deflection = 0f;
        
        [SerializeField]
        private Transform Origin;

        public void Spawn(GameObject obstacle)
        {
            var newObstacle = Instantiate(obstacle, transform);

            newObstacle.transform.localEulerAngles += Vector3.up * Deflection;

            newObstacle.transform.parent = Origin;
        }
    }
}