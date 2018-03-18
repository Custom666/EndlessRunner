using UnityEngine;

namespace Assets.Scripts
{
    /// <inheritdoc />
    /// <summary>
    /// Planet rotation controller
    /// </summary>
    public class PlanetRotor : MonoBehaviour
    {
        public float Speed;

        // Update is called once per frame
        private void FixedUpdate()
        {
            transform.Rotate(Vector3.forward * Speed * Time.deltaTime);
        }
    }
}
