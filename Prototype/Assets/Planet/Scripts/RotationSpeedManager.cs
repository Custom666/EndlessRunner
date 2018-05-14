using System.Collections;
using Assets.Player.Scripts;
using UnityEngine;

namespace Assets.Planet.Scripts
{
    /// <summary>
    /// Manager that handle world speed rotation (texture, jump, gravity, animation ...)
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class RotationSpeedManager : MonoBehaviour
    {
        public float Speed = 35f;

        [SerializeField]
        private float Interval = 8f;
        [SerializeField]
        private float SpeedIncrement = 4f;
        [SerializeField]
        private float AnimationSpeedIncrement = 0.1f;
        [SerializeField]
        private float PlayerJumpIncrement = 3f;

        [SerializeField]
        private Vector3 GravityIncrement = new Vector3(0f, .35f, 0f);

        [SerializeField]
        private PlayerController Player;

        private Animator _playerAnimator;

        private void Start()
        {
            _playerAnimator = Player.GetComponent<Animator>();

            StartCoroutine(increaseSpeedCoroutine());
        }

        private IEnumerator increaseSpeedCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(Interval);

                Speed += SpeedIncrement;

                _playerAnimator.speed += AnimationSpeedIncrement;

                Physics.gravity -= GravityIncrement;

                Player.Jump += PlayerJumpIncrement;
            }
        }
    }
}
