using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class PlanetRotation : MonoBehaviour
    {
        public float Deflection = 30;
        public float Speed = 5;

        public bool CanRotate = true;

        private float _horizontalMove;

        private void Update()
        {
            if (!CanRotate) return;

            if (Input.GetKeyDown(KeyCode.LeftArrow)) _horizontalMove = Mathf.Clamp(_horizontalMove - 1, -1, 1);
            if (Input.GetKeyDown(KeyCode.RightArrow)) _horizontalMove = Mathf.Clamp(_horizontalMove + 1, -1, 1);
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!CanRotate) return;

            var z = Mathf.Clamp(_horizontalMove * Deflection, -90, 90);

            transform.localEulerAngles = new Vector3
                (
                    transform.localEulerAngles.x, 
                    transform.localEulerAngles.y, 
                    Mathf.LerpAngle(transform.localEulerAngles.z, z, Time.deltaTime * Speed)
                );
        }
    }
}
