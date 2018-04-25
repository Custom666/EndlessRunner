using System;
using Assets.Weapon.Scripts;
using UnityEngine;

namespace Assets.Enemies.Scripts
{
        
    public class EnemyController : MonoBehaviour
    {
        public float ShootDistance = 15f;
        public float FireInterval = 2f;

        private float _lastFireTime;

        private WeaponController _weaponController;

        private void Start()
        {
            _weaponController = transform.GetChild(0).gameObject.GetComponent<WeaponController>();
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            var player = FindObjectOfType(typeof(Player.Scripts.PlayerController)) as Player.Scripts.PlayerController;

            if(player == null || transform.position.z < player.transform.position.z) return;
            
            var distance = (transform.position - player.transform.position).magnitude;
            
            // Enemy is in shootable area
            if (!(distance < ShootDistance)) return;

            // look at player with weapon
            transform.GetChild(0).LookAt(player.transform);
            
            // is too soon for another fire 
            //if (!(Math.Abs(Time.fixedTime - _lastFireTime) >= FireInterval)) return;

            _weaponController.Fire();

            _lastFireTime = Time.fixedTime;
        }
    }
}
