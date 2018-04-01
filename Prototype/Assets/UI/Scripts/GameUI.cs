using System;
using Assets.Enemies.Scripts;
using Assets.Player.Scripts;
using Assets.Projectiles.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.UI.Scripts
{
    public class GameUI : MonoBehaviour
    {
        private Text _playerHealth;

        private Slider _enemyHealth;

        private GameObject _gameMenu;

        private void Start()
        {
            _playerHealth = transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Text>();
            _enemyHealth = transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Slider>();
            _gameMenu = transform.GetChild(2).gameObject;

            _enemyHealth.onValueChanged.AddListener((value) =>
            {
                transform.GetChild(1).transform.GetChild(1).gameObject.GetComponent<Text>().text = "Enemy: " + value;
            });
        }

        private void LateUpdate()
        {
            if(Input.GetButtonDown("Pause")) _gameMenu.SetActive(true);
        }

        private void OnEnable()
        {
            PlayerController.OnHealthChangedEvent += PlayerOnHealthChangedEvent;
            ProjectileController.OnEnemyHitEvent += ProjectileOnEnemyHitEvent;
        }

        private void OnDisable()
        {
            PlayerController.OnHealthChangedEvent -= PlayerOnHealthChangedEvent;
            ProjectileController.OnEnemyHitEvent -= ProjectileOnEnemyHitEvent;
        }

        private void PlayerOnHealthChangedEvent(int health)
        {
            _playerHealth.text = "Healths: " + health;

            if (health <= 0)
            {
                Time.timeScale = 0f;

                _gameMenu.transform.Find("GameMenuText").GetComponent<Text>().text = "GAME OVER";

                _gameMenu.SetActive(true);
            }
        }

        private void ProjectileOnEnemyHitEvent()
        {
            _enemyHealth.value -= 1;

            if (_enemyHealth.value <= 0)
            {
                Time.timeScale = 0f;

                _gameMenu.transform.Find("GameMenuText").GetComponent<Text>().text = "VICTORY";

                _gameMenu.SetActive(true);
            }
        }
    }
}
