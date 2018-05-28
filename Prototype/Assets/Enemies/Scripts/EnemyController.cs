using UnityEngine;

namespace Assets.Enemies.Scripts
{
    /// <summary>
    /// Controller of game enemy player
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EnemyController : MonoBehaviour
    {
        //[SerializeField] private float ShootDistance = 15f;
        //[SerializeField] private float FireInterval = 2f;
        
        //private float _lastFireTime;
        //private WeaponController _weaponController;

        //private void Awake()
        //{
        //    _audio = GetComponent<AudioSource>();
        //}
        
        //private void Start()
        //{
        //    _weaponController = transform.GetChild(0).gameObject.GetComponent<WeaponController>();
        //}

        // TODO GAME DESIGH DOCUMENT
        // For now is not in use
        //private void FixedUpdate()
        //{
        //    var player = FindObjectOfType(typeof(Player.Scripts.PlayerController)) as Player.Scripts.PlayerController;

        //    if (player == null || transform.position.z < player.transform.position.z) return;

        //    var distance = (transform.position - player.transform.position).magnitude;

        //    // Enemy is in shootable area
        //    if (!(distance < ShootDistance)) return;

        //    // look at player with weapon
        //    transform.GetChild(0).LookAt(player.transform);

        //    // is too soon for another fire 
        //    //if (!(Math.Abs(Time.fixedTime - _lastFireTime) >= FireInterval)) return;

        //    _weaponController.Fire();

        //    _lastFireTime = Time.fixedTime;
        //}
    }
}
