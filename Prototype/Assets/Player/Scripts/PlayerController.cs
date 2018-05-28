using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Planet.Scripts;
using Assets.Weapon.Scripts;
using UnityEngine;

namespace Assets.Player.Scripts
{
    /// <summary>
    /// Controller responsible for player mechanics.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(WeaponController))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(AudioSource))]
    public class PlayerController : MonoBehaviour
    {
        private float _health;
        private Rigidbody _rigidbody;
        private WeaponController _weaponController;
        private Animator _animator;
        private IDictionary<string, AudioSource> _audios;

        [SerializeField] private PlanetRotation _planet;
        [SerializeField] private float _enemyDamage = 15f;
        [SerializeField] private float _obstacleDamage = 10f;
        [SerializeField] private float _maxHealth = 80f;

        public float Jump = 30f;
        public float JumpSpeed = 5f;
        public float FallSpeed = 4f;

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

        public List<AudioSource> Audios
        {
            get
            {
                var audios = _audios.Values.ToList();

                audios.Add(_weaponController.GetComponent<AudioSource>());

                return audios;
            }
        }

        public delegate void OnHealthChanged(float health);
        public delegate void OnReceiveDamage();

        public static event OnHealthChanged OnHealthChangedEvent;
        public static event OnReceiveDamage OnReceiveDamageEvent;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _animator = GetComponent<Animator>();

            _weaponController = transform.GetChild(0).gameObject.GetComponent<WeaponController>();

            _audios = new Dictionary<string, AudioSource>();

            var audios = GetComponents<AudioSource>();

            _audios.Add("jump", audios.First(a => string.Compare(a.clip.name, "player_jump", StringComparison.Ordinal) == 0));
            _audios.Add("hurt", audios.First(a => string.Compare(a.clip.name, "player_hurt", StringComparison.Ordinal) == 0));
        }

        public void Start()
        {
            Health = _maxHealth;
        }

        private void Update()
        {
            if (GameStateHelper.GameState == GameState.GameOver)
            {
                if (_audios["jump"].isPlaying) _audios["jump"].Stop();

                return;
            }

            Health -= Time.deltaTime;

            if (Input.GetButtonDown("Fire")) _weaponController.Fire();

            if (isGrounded())
            {
                _animator.SetBool("IsGrounded", true);

                if (_audios["jump"].isPlaying) _audios["jump"].Stop();

                _planet.CanRotate = true;

                if (Input.GetButtonDown("Jump")) _rigidbody.velocity = Vector3.up * Jump;
            }
            else
            {
                _planet.CanRotate = false;

                _animator.SetBool("IsGrounded", false);

                if (!_audios["jump"].isPlaying) _audios["jump"].Play();
            }
        }

        private void FixedUpdate()
        {
            if (_rigidbody.velocity.y < 0)
                _rigidbody.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime * FallSpeed;
            else if (_rigidbody.velocity.y > 0)
                _rigidbody.velocity += Vector3.up * Physics.gravity.y * Time.deltaTime * JumpSpeed;
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

                    StartCoroutine(FallIntoCraterDeath());

                    break;
            }

            _audios["hurt"].Play();

            _animator.SetTrigger("GettingDamage");

            if (OnReceiveDamageEvent != null) OnReceiveDamageEvent();
        }

        private IEnumerator FallIntoCraterDeath()
        {
            Time.timeScale = 0.1f;

            var yPosition = transform.position.y;

            yield return new WaitUntil(() =>
            {
                _rigidbody.transform.position -= new Vector3(0f, 0.2f, 0f);

                return Math.Abs(_rigidbody.transform.position.y - yPosition) > 5f;
            });
            
            Health = 0f;
        }
    }
}
