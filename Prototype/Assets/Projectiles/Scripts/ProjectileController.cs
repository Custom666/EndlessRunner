using Assets.Player.Scripts;
using UnityEngine;

namespace Assets.Projectiles.Scripts
{
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
