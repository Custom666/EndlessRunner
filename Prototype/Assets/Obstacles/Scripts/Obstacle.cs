using UnityEngine;

namespace Assets.Obstacles.Scripts
{
    /// <summary>
    /// Obstacle model that encapsulate trigger logic
    /// </summary>
    public class Obstacle : MonoBehaviour
    {
        /// <summary>
        /// Determine if player can jump over this obstacle
        /// </summary>
        [SerializeField] public bool IsTransportable = false;

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.name)
            {
                case "Destroyer":
                    
                    Destroy(gameObject, 1f);

                    break;
            }
        }
    }
}
