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

        public float Jump = 5f;
        
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
                
                if (Input.GetButtonDown("Jump"))
                {
                    var force = Vector2.up * Jump;
                    
                    Debug.Log(force);
                    _rigidbody.AddForce(force, ForceMode.Impulse);
                }
            }           
            else Planet.CanRotate = false;
        }
        
        private bool isGrounded()
        {
            return Physics.Raycast(transform.position + new Vector3(transform.localScale.x / 2f, -transform.localScale.y / 2f, 0f), Vector3.down, 1f) ||
                   Physics.Raycast(transform.position - new Vector3(transform.localScale.x / 2f, transform.localScale.y / 2f, 0f), Vector3.down, 1f);
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
