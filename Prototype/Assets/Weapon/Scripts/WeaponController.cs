using System.Collections;
using UnityEngine;

namespace Assets.Weapon.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject Projectile;
        public Transform Parent;
        public Transform Spawn;

        public float Speed = 30f;
        public float Time = 3f;

        private int _lastFireTime;

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetButtonDown("Fire") && _lastFireTime < (int)UnityEngine.Time.fixedTime) fire();
        }

        private IEnumerator destroyProjectile(GameObject projectile, float delay)
        {
            yield return new WaitForSeconds(delay);

            Destroy(projectile);
        }

        private void fire()
        {
            var projectile = Instantiate(Projectile, Spawn.position, Projectile.transform.rotation, Parent);

            Physics.IgnoreCollision(projectile.GetComponent<Collider>(), Spawn.parent.GetComponent<Collider>());
            
            projectile.GetComponent<Rigidbody>().AddForce(Spawn.forward * Speed, ForceMode.Impulse);

            _lastFireTime = (int) UnityEngine.Time.fixedTime;

            StartCoroutine(destroyProjectile(projectile, Time));
        }
    }
}
