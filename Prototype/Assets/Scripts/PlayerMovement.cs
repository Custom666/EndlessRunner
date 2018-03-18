using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        #region PUBLIC FIELDS

        public float Speed;
        public float Jump;
        
        public BoundaryModel Boundaries;

        public FauxGravityAttractor Attractor;

        #endregion  

        private Rigidbody _rigidbody;
        private Animator _animator;

        private Vector3 _move;

        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        // Update is called once per frame
        private void Update()
        {
            _move = new Vector3(Input.GetAxis("Horizontal"), 0, 1);
        }
        
        private void FixedUpdate()
        {
            var tempPosition = _rigidbody.position;

            tempPosition += transform.TransformDirection(_move * Speed * Time.deltaTime);

            _rigidbody.position = new Vector3
                (
                    Mathf.Clamp(tempPosition.x, Boundaries.XMin, Boundaries.XMax),
                    tempPosition.y,
                    tempPosition.z
                );

            Attractor.Attract(transform);
        }
    }
}
