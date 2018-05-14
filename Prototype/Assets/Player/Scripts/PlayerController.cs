using System;
using Assets.Enemies.Scripts;
using Assets.Planet.Scripts;
using Assets.Weapon.Scripts;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

namespace Assets.Player.Scripts
{
    /// <summary>
    /// Controller responsible for player mechanics.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(WeaponController))]
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public delegate void OnHealthChanged(int health);

        public static event OnHealthChanged OnHealthChangedEvent;
        
        public float Jump = 15f;

        [SerializeField]
        private PlanetRotation Planet;

        private Rigidbody _rigidbody;
        private WeaponController _weaponController;
        private Animator _animator;

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

            _animator = GetComponent<Animator>();

            _weaponController = transform.GetChild(0).gameObject.GetComponent<WeaponController>();

            Health = 5;
        }
        
        private void Update()
        {
            if (Input.GetButtonDown("Fire")) _weaponController.Fire();

            if (isGrounded())
            {
                Planet.CanRotate = true;

                if (Input.GetButtonDown("Jump"))
                {
                    var force = Vector3.up * Jump;
                    
                    _rigidbody.AddForce(force, ForceMode.Impulse);

                    // TODO change bool to float
                    _animator.SetBool("Jump", true);
                }
                else _animator.SetBool("Jump", false);
                    
            }
            else
            {
                Planet.CanRotate = false;
            }
        }
        
        private bool isGrounded()
        {
            return Physics.Raycast(transform.position, Vector3.down, .2f);
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
