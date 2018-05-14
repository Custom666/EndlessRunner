using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Obstacles.Scripts
{
    /// <summary>
    /// Manager that use spawn controller to spawn randomly obstacles from collection. 
    /// It guaranted that one of them will always be transportable.
    /// </summary>
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField]
        private List<SpawnController> Controllers;

        // Update is called once per frame
        private void FixedUpdate()
        {
            // TODO domyslet spawn interval 
            if (Math.Abs(Time.fixedTime % Controllers[0].SpawnInterval) > 0.01f) return;

            var obstacles = new List<GameObject>();

            var transportableObstacles = new List<GameObject>();

            foreach (var controller in Controllers)
            {
                var randomIndex = Random.Range(0, controller.Obstacles.Count);

                obstacles.Add(controller.Obstacles[randomIndex]);

                transportableObstacles.AddRange(controller.Obstacles.FindAll(o => o.GetComponent<Obstacle>().IsTransportable));
            }

            if (!obstacles.Any(o => o.GetComponent<Obstacle>().IsTransportable))
            {
                obstacles[Random.Range(0, obstacles.Count)] = transportableObstacles[Random.Range(0, transportableObstacles.Count)];
            }

            for (var i = 0; i < Controllers.Count; i++) Controllers[i].Spawn(obstacles[i]);
        }
    }
}
