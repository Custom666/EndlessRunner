using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.UI.Scripts
{
    /// <summary>
    /// User interface in passive game
    /// </summary>
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private GameObject _gameMenu;
        [CanBeNull] [SerializeField] private Sprite _victoryImage;
        [CanBeNull] [SerializeField] private Sprite _gameOverImage;

        private Image _menuBackground;
        private Sprite _backgroundImage;

        private void Start()
        {
            if (_gameMenu == null) return;

            _menuBackground = _gameMenu.GetComponent<Image>();
            _backgroundImage = _menuBackground.sprite;
        }

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
            if(GameStateHelper.GameState != GameState.Menu) GameStateHelper.GameState = GameState.Pause;

            ActivateGameMenu(true);

            Time.timeScale = 0f;
        }

        public void Resume()
        {
            if (GameStateHelper.GameState != GameState.Menu) GameStateHelper.GameState = GameState.Playing;

            ActivateGameMenu(false);

            Time.timeScale = 1f;
        }

        public void Restart()
        {
            PlayLevel(SceneManager.GetActiveScene().name);
        }

        public void GameOver(bool victory)
        {
            GameStateHelper.GameState = victory ? GameState.Victory : GameState.GameOver;

            ActivateGameMenu(true);

            Time.timeScale = 0f;
        }

        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            GameStateHelper.GameState = scene.name.ToLower().Contains("level") ? GameState.Playing : GameState.Menu;
        }

        private void ActivateGameMenu(bool active)
        {
            if (_victoryImage != null && _gameOverImage != null)
                _menuBackground.sprite = GameStateHelper.GameState == GameState.Victory
                    ? _victoryImage
                    : GameStateHelper.GameState == GameState.GameOver
                        ? _gameOverImage
                        : _backgroundImage;

            _gameMenu.SetActive(active);
        }
        
        private void OnGameStateChangingEvent()
        {
            if (_gameMenu == null) return;

            var buttons = _gameMenu.GetComponentsInChildren<Button>();
            
            switch (GameStateHelper.GameState)
            {
                case GameState.Menu:

                    foreach (var button in buttons)
                        button.gameObject.SetActive(
                            !button.name.ToLower().Contains("restart") &&
                            !button.name.ToLower().Contains("replay") &&
                            !button.name.ToLower().Contains("menu"));

                    break;

                case GameState.Pause:

                    foreach (var button in buttons)
                        button.gameObject.SetActive(
                            !button.name.ToLower().Contains("quit") &&
                            !button.name.ToLower().Contains("replay"));

                    break;

                case GameState.GameOver:
                    // same rules as victory
                case GameState.Victory:

                    foreach (var button in buttons) button.gameObject.SetActive(
                        !button.name.ToLower().Contains("resume") &&
                        !button.name.ToLower().Contains("quit"));

                    break;
            }
        }
    }
}
