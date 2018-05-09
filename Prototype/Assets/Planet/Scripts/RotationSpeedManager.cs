using System.Collections;
using Assets.Player.Scripts;
using UnityEngine;

namespace Assets.Planet.Scripts
{
    public class RotationSpeedManager : MonoBehaviour
    {
        public float Speed = 35f;
        public float Interval = 8f;
        public float SpeedIncrement = 4f;
        public float AnimationSpeedIncrement = 0.1f;
        public float PlayerJumpIncrement = 3f;

        public Vector3 GravityIncrement = new Vector3(0f, .35f, 0f);

        public PlayerController Player;

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
