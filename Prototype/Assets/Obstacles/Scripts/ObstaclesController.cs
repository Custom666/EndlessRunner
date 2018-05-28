using Assets.Planet.Scripts;
using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    /// <summary>
    /// Obstacle controller rotate children around self
    /// </summary>
    public class ObstaclesController : MonoBehaviour
    {
        [SerializeField]
        private RotationSpeedManager SpeedManager;

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.left, SpeedManager.Speed * Time.deltaTime, Space.Self);
        }
    }
}
