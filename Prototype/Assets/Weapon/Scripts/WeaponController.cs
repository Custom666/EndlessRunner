using System.Collections;
using UnityEngine;

namespace Assets.Weapon.Scripts
{
    /// <summary>
    /// Controller that describe weapon mechanics
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class WeaponController : MonoBehaviour
    {
        [SerializeField] private GameObject _projectile;
        [SerializeField] private float _speed = 15f;
        [SerializeField] private float _aliveTime = 5f;

        private AudioSource _shootAudio;
        private int _lastFireTime;

        private void Awake()
        {
            _shootAudio = GetComponent<AudioSource>();
        }

        public void Fire()
        {
            if (_lastFireTime >= (int) Time.fixedTime) return;

            var parent = GameObject.Find("MovablePlanet");

            var projectile = Instantiate(_projectile, transform.position, _projectile.transform.rotation, parent.transform);

            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
            
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * _speed, ForceMode.Impulse);

            _shootAudio.Play();

            _lastFireTime = (int) Time.fixedTime;
            
            Destroy(projectile, _aliveTime);
        }
    }
}
