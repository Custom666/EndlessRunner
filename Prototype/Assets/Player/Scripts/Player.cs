using System;
using Assets.Enemies.Scripts;
using Assets.Planet.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace Assets.Player.Scripts
{
    public class Player : MonoBehaviour
    {
        public float Jump = 5f;

        public Text HealthText;

        public PlanetRotation Planet;

        public GameObject GameOverPanel;

        private Rigidbody _rigidbody;
        private Animator _animator;
        
        private int _health = 5;
        
        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();
           
            HealthText.text = _health.ToString();
        }

        private void FixedUpdate()
        {
            if (isGrounded())
            {
                Planet.CanRotate = true;

                if (Input.GetButtonDown("Fire")) fire();

                if (Input.GetButtonDown("Jump")) _rigidbody.AddForce(Vector2.up * Jump, ForceMode.Impulse);
            }           
            else Planet.CanRotate = false;
        }
        
        private void fire()
        {
            Debug.DrawRay(transform.position + new Vector3(0.5f, 0f, 0f), Vector3.forward * 10f, Color.red, 0.5f);

            RaycastHit hitObject;

            if (Physics.Raycast(transform.position + new Vector3(0.5f, 0f, 0f), Vector3.forward, out hitObject, 10f)
                && hitObject.transform.gameObject.GetComponent<EnemyController>() != null)
                Destroy(hitObject.transform.gameObject);
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

                    _health -= 1;

                    HealthText.text = _health.ToString();

                    if (_health <= 0)
                    {
                        Time.timeScale = 0f;

                        GameOverPanel.SetActive(true);
                    }

                    break;
                case "Crater":

                    _health = 0;

                    HealthText.text = _health.ToString();

                    Time.timeScale = 0f;

                    GameOverPanel.SetActive(true);

                    break;
            }
        }
    }
}
