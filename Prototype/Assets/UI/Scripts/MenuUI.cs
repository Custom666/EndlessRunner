using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Enemies.Scripts;
using Assets.Player.Scripts;
using Assets.Projectiles.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Assets.UI.Scripts
{
    /// <summary>
    /// User interface in passive game
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        [CanBeNull] [SerializeField] private GameObject _gameMenu;
        
        private Button _gameMenuResumeButton;
        
        private void Awake()
        {
            if (_gameMenu == null) return;
            
            _gameMenuResumeButton = _gameMenu.GetComponentsInChildren<Button>()
                .FirstOrDefault(child => string.Compare(child.name, "ResumeButton", StringComparison.Ordinal) == 0);
        }

        private void Update()
        {
            if (Input.GetButtonDown("Pause") && !GameState.IsGameOver)
            {
                if (!GameState.IsPause) Pause();
                else Resume();
            }
        }

        public void Quit()
        {
            Application.Quit();    
        }
        
        public void PlayLevel(string name)
        {
            if (!Application.CanStreamedLevelBeLoaded(name))
                throw new FileNotFoundException(string.Format("Scene with name {0} do not exist"), name);

            GameState.IsGameOver = false;
            GameState.IsPause = false;

            Time.timeScale = 1f;

            SceneManager.LoadScene(name);
        }

        public void Pause()
        {
            GameState.IsPause = true;

            _gameMenu.SetActive(true);

            Time.timeScale = 0f;
        }

        public void Resume()
        {
            GameState.IsPause = false;

            _gameMenu.SetActive(false);

            Time.timeScale = 1f;
        }

        public void Restart()
        {
            PlayLevel(SceneManager.GetActiveScene().name);
        }
        
        public void GameOver()
        {
            if (GameState.IsGameOver) return;

            GameState.IsGameOver = true;
            
            if (_gameMenu != null)
            {
                _gameMenuResumeButton.gameObject.SetActive(false);

                _gameMenu.SetActive(true);
            }
            
            Time.timeScale = 0f;
        }

        public void ToggleSound(AudioSource sound)
        {
            
        }

        public void ToggleMusic(AudioSource music)
        {

        }
    }
}
