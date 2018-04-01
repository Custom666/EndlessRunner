using UnityEngine;

namespace Assets.Enemies.Scripts
{
        
    public class EnemyController : MonoBehaviour
    {
        // Update is called once per frame
        private void FixedUpdate()
        {
            var player = FindObjectOfType(typeof(Player.Scripts.PlayerController)) as Player.Scripts.PlayerController;

            if (player != null) Debug.DrawRay(
                transform.position + new Vector3(0f, 2.7f, 0f), 
                player.gameObject.transform.position - transform.position - new Vector3(0f, 2.7f, 0f), 
                Color.yellow);
        }
    }
}
