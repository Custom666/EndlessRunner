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
        [SerializeField] private GameObject _gameMenu;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            GameStateHelper.OnGameStateChangingEvent += OnGameStateChangingEvent;
        }
        
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
            GameStateHelper.OnGameStateChangingEvent -= OnGameStateChangingEvent;
        }

        private void Update()
        {
            if (GameStateHelper.GameState == GameState.GameOver) return;

            if (Input.GetButtonDown("Pause"))
            {
                if (GameStateHelper.GameState == GameState.Pause) Resume();
                else if (GameStateHelper.GameState != GameState.Pause) Pause();
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

            Time.timeScale = 1f;

            SceneManager.LoadScene(name);
        }

        public void Pause()
        {
            if (_gameMenu == null) return;

            GameStateHelper.GameState = GameState.Pause;

            _gameMenu.SetActive(true);

            Time.timeScale = 0f;
        }

        public void Resume()
        {
            if (_gameMenu == null) return;

            GameStateHelper.GameState = GameState.Playing;

            _gameMenu.SetActive(false);

            Time.timeScale = 1f;
        }

        public void Restart()
        {
            PlayLevel(SceneManager.GetActiveScene().name);
        }

        public void GameOver()
        {
            GameStateHelper.GameState = GameState.GameOver;
            
            Time.timeScale = 0f;
        }
        
        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            GameStateHelper.GameState = scene.name.ToLower().Contains("level") ? GameState.Playing : GameState.Menu;
        }

        private void OnGameStateChangingEvent()
        {
            if(_gameMenu == null) return;

            var buttons = _gameMenu.GetComponentsInChildren<Button>();

            switch (GameStateHelper.GameState)
            {
                case GameState.Menu:

                    foreach (var button in buttons)
                        button.gameObject.SetActive(
                            !button.name.ToLower().Contains("restart") && 
                            !button.name.ToLower().Contains("menu"));
                    
                    break;

                case GameState.Pause:

                    foreach (var button in buttons) button.gameObject.SetActive(true);
                    
                    break;

                case GameState.GameOver:

                    foreach (var button in buttons) button.gameObject.SetActive(!button.name.ToLower().Contains("resume"));

                    break;
            }
        }
    }
}
