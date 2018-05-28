using System.Collections;
using Assets.Player.Scripts;
using UnityEngine;

namespace Assets.Planet.Scripts
{
    /// <summary>
    /// Manager that handle world speed rotation (texture, jump, gravity, animation ...)
    /// </summary>
    public class RotationSpeedManager : MonoBehaviour
    {
        public float Speed = 35f;

        [SerializeField] private float _maxSpeed = 75f;
        [SerializeField] private float _interval = 7f;
        [SerializeField] private float _speedIncrement = 5f;
        [SerializeField] private float _animationSpeedIncrement = 0.1f;
        [SerializeField] private float _playerJumpIncrement = 3f;
        [SerializeField] private PlayerController _player;

        private Animator _playerAnimator;
        private float _playerJumpSpeedIncrement;
        private float _playerFallSpeedIncrement;

        private void Awake()
        {
            _playerAnimator = _player.GetComponent<Animator>();

            var difference = (_player.Jump + _playerJumpIncrement) / _player.Jump;

            _playerJumpSpeedIncrement = difference * _player.JumpSpeed - _player.JumpSpeed;
            _playerFallSpeedIncrement = difference * _player.FallSpeed - _player.FallSpeed;
        }

        private void Start()
        {
            StartCoroutine(increaseSpeedCoroutine());
        }

        private IEnumerator increaseSpeedCoroutine()
        {
            while (Speed < _maxSpeed)
            {
                Speed += _speedIncrement;

                _playerAnimator.speed += _animationSpeedIncrement;
                
                _player.Jump += _playerJumpIncrement;
                _player.JumpSpeed += (_playerJumpSpeedIncrement + 1f);
                _player.FallSpeed += (_playerFallSpeedIncrement + 1f);

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}
