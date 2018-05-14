using System.Collections;
using UnityEngine;

namespace Assets.Weapon.Scripts
{
    /// <summary>
    /// Controller that describe weapon mechanics
    /// </summary>
    public class WeaponController : MonoBehaviour
    {
        [SerializeField]
        private GameObject Projectile;

        [SerializeField]
        private float Speed = 15f;

        [SerializeField]
        private float Time = 5f;

        private int _lastFireTime;
        
        public void Fire()
        {
            if (_lastFireTime >= (int) UnityEngine.Time.fixedTime) return;

            var parent = GameObject.Find("MovablePlanet");

            var projectile = Instantiate(Projectile, transform.position, Projectile.transform.rotation, parent.transform);

            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
            
            projectile.GetComponent<Rigidbody>().AddForce(transform.forward * Speed, ForceMode.Impulse);

            _lastFireTime = (int) UnityEngine.Time.fixedTime;
            
            Destroy(projectile, Time);
        }
    }
}
