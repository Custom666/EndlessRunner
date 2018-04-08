using System;
using Assets.Enemies.Scripts;
using Assets.Planet.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace Assets.Player.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        public delegate void OnHealthChanged(int health);

        public static event OnHealthChanged OnHealthChangedEvent;

        public float Jump = 15f;

        public Vector3 AddedGravity = new Vector3(0f, -1f, 0f); 

        public PlanetRotation Planet;

        public GameObject GameOverPanel;

        private Rigidbody _rigidbody;
        private int _health;

        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                if (OnHealthChangedEvent != null) OnHealthChangedEvent(_health);
            }
        }

        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            Health = 5;
        }

        private void FixedUpdate()
        {
            if (isGrounded())
            {
                Planet.CanRotate = true;
                Physics.gravity = Vector3.down * 9.81f;

                if (Input.GetButtonDown("Jump"))
                {
                    var force = Vector3.up * Jump;

                    Debug.Log(force);
                    _rigidbody.AddForce(force, ForceMode.Impulse);
                }
            }
            else
            {
                Planet.CanRotate = false;

                Physics.gravity += AddedGravity;
            }
        }
        
        private bool isGrounded()
        {
            Debug.DrawRay(transform.position + new Vector3(0f, -transform.localScale.y / 2f, 0f), Vector3.down, Color.cyan, .5f);
            return Physics.Raycast(transform.position + new Vector3(0f, -transform.localScale.y / 2f, 0f), Vector3.down, .5f);
        }

        private void OnTriggerEnter(Collider other)
        {
            switch (other.gameObject.tag)
            {
                default: return;

                case "Obstacle":

                    Health -= 1;
                    
                    break;
                case "Enemy":

                    Health -= 1;

                    break;
                case "Crater":

                    Health = 0;

                    break;
            }
        }
    }
}
