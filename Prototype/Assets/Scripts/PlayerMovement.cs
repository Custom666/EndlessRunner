using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        #region PUBLIC FIELDS

        public float Speed;
        public float Jump;
        
        public BoundaryModel Boundaries;

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
            transform.position += transform.TransformDirection(_move * Speed * Time.deltaTime);

            var x = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)
                ? Mathf.Clamp(transform.position.x, Boundaries.XMin, Boundaries.XMax)
                : Mathf.Lerp(transform.position.x, 0f, Time.deltaTime);

            transform.position = new Vector3
                (
                    x,
                    transform.position.y,
                    transform.position.z
                );
            
            if (Input.GetButtonDown("Jump"))
                _rigidbody.AddForce(Vector3.up * Jump * (transform.position.y > 0f ? 1f : -1f), ForceMode.Impulse);
        }
    }
}
