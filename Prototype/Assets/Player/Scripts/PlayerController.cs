using System;
using UnityEngine;

namespace Assets.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float Jump = 5f;

        private Rigidbody2D _rigidbody;
        private Animator _animator;

        private float _distToGround;
        private bool _isGrounded = true;

        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();

            _distToGround = GetComponent<Collider2D>().bounds.extents.y;
        }

        private void FixedUpdate()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
                _isGrounded = false;
            }
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.CompareOrdinal("Planet", other.gameObject.name) == 0) _isGrounded = true;
        }
    }
}
