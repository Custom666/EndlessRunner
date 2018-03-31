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
        
        // Update is called once per frame
        private void FixedUpdate()
        {
            if (Math.Abs(Time.fixedTime % SpawnInterval) > 0.01f) return;

            var randomIndex = Random.Range(0, Obstacles.Count + 1);
            
            if(randomIndex == Obstacles.Count) return;
            
            var obstacle = Instantiate(Obstacles[randomIndex], transform);

            obstacle.transform.localEulerAngles += Vector3.forward * Deflection;

            obstacle.transform.parent = Origin;
        }
    }
}