using System;
using Assets.Planet.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public float Jump = 5f;

        public Text HealthText;

        public PlanetRotation Planet;

        public GameObject GameOverPanel;

        private Rigidbody _rigidbody;
        private Animator _animator;
        
        private bool _isGrounded = true;
        private int _health = 5;

        public bool IsGrounded { set { Planet.CanRotate = _isGrounded = value; } }

        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
           
            HealthText.text = _health.ToString();
        }

        private void FixedUpdate()
        {
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                _rigidbody.AddForce(Vector2.up * Jump, ForceMode.Impulse);
                IsGrounded = false;
            }
        }
        
        private void OnCollisionEnter(Collision other)
        {
            if (string.CompareOrdinal("Planet", other.gameObject.name) == 0) IsGrounded = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Obstacle"))
            {
                _health -= 1;

                HealthText.text = _health.ToString();

                if (_health <= 0)
                {
                    Time.timeScale = 0f;

                    GameOverPanel.SetActive(true);
                }
            }
        }
    }
}
