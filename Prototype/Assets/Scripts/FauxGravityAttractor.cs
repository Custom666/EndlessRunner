using System;
using UnityEngine;

namespace Assets.Scripts
{
    /// <inheritdoc />
    /// <summary>
    /// Spherical planet gravity attractor
    /// </summary>
    public class FauxGravityAttractor : MonoBehaviour
    {
        public float Gravity = -10;

        // Use this for initialization
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        /// <summary>
        /// Attract given object
        /// </summary>
        /// <param name="objecTransform">Object to attract</param>
        public void Attract(Transform objecTransform)
        {
            var gravityUp = (objecTransform.position - transform.position).normalized;
            var objectRigidbody = objecTransform.GetComponent<Rigidbody>();

            if (objectRigidbody == null)
            {
                Debug.LogError("Object rigidbody is null!");
                throw new NullReferenceException("Object rigidbody is null!");
            }

            objectRigidbody.AddForce(gravityUp * Gravity);

            var targetRotation = Quaternion.FromToRotation(objecTransform.up, gravityUp) * objecTransform.rotation;

            objecTransform.rotation = Quaternion.Slerp(objecTransform.rotation, targetRotation, 50f * Time.deltaTime);
        }
    }
}