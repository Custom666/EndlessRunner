using UnityEngine;

namespace Assets.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float Jump = 5f;

        private Rigidbody _rigidbody;
        private Animator _animator;

        private float _distToGround;

        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _distToGround = GetComponent<Collider>().bounds.extents.y;
        }

        private void FixedUpdate()
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
                _rigidbody.AddForce(Vector3.up * Jump, ForceMode.Impulse);
        }

        public bool IsGrounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, _distToGround);
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }
    }
}
