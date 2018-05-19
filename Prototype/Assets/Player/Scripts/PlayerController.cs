using System;
using System.ComponentModel;
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
        private float _health;
        private Rigidbody _rigidbody;
        private WeaponController _weaponController;
        private Animator _animator;

        [SerializeField] private PlanetRotation _planet;

        [SerializeField] private float _enemyDamage = 15f;

        [SerializeField] private float _obstacleDamage = 10f;

        [SerializeField] private float _maxHealth = 80f;

        public float Jump = 10f;

        public float MaxHealth { get { return _maxHealth; } }

        public float Health
        {
            get { return _health; }
            set
            {
                _health = value;

                if (OnHealthChangedEvent != null) OnHealthChangedEvent(_health);
            }
        }

        public delegate void OnHealthChanged(float health);

        public delegate void OnReceiveDamage();

        public static event OnHealthChanged OnHealthChangedEvent;

        public static event OnReceiveDamage OnReceiveDamageEvent;
        
        // Use this for initialization
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _animator = GetComponent<Animator>();

            _weaponController = transform.GetChild(0).gameObject.GetComponent<WeaponController>();

            Health = _maxHealth;
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire")) _weaponController.Fire();

            if (isGrounded())
            {
                _planet.CanRotate = true;

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
                _planet.CanRotate = false;
            }
        }

        private void FixedUpdate()
        {
            if (GameState.IsGameOver) return;

            Health -= Time.deltaTime;
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

                    Health -= _obstacleDamage;

                    break;
                case "Enemy":

                    Health -= _enemyDamage;

                    break;
                case "Crater":

                    Health = 0f;

                    break;
            }

            if (OnReceiveDamageEvent != null) OnReceiveDamageEvent();
        }
    }
}
