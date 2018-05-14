using Assets.Player.Scripts;
using Assets.Weapon.Scripts;
using UnityEngine;

namespace Assets.Projectiles.Scripts
{
    /// <summary>
    /// Controller that encapsulate logic for <see cref="WeaponController"/> bullet
    /// </summary>
    public class ProjectileController : MonoBehaviour
    {
        public delegate void OnEnemyHit();
        
        public static event OnEnemyHit OnEnemyHitEvent;
        
        private void OnTriggerEnter(Collider collider)
        {
            switch (collider.gameObject.tag)
            {
                case "Enemy":

                    if (OnEnemyHitEvent != null) OnEnemyHitEvent();

                    Destroy(collider.gameObject);

                    break;

                case "Player":

                    var player = GameObject.Find("Player");

                    if(player == null) break;

                    player.GetComponent<PlayerController>().Health--;
                    
                    break;

            }

            Destroy(gameObject);
        }
    }
}
