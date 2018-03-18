using UnityEngine;

namespace Assets.Scripts
{
    /// <inheritdoc />
    /// <summary>
    /// Main game cameras controller for camera transforming to specific object
    /// </summary>
    public class FollowCamera : MonoBehaviour
    {   
        private void LateUpdate()
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
        }
    }
}
