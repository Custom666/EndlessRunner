using System;
using Assets.Enemies.Scripts;
using Assets.Player.Scripts;
using Assets.Projectiles.Scripts;
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
        private void LateUpdate()
        {
            if (Input.GetButtonDown("Pause")) Pause(gameObject);
        }

        public void Quit()
        {
            Application.Quit();    
        }

        public void LoadIntro()
        {
            SceneManager.LoadSceneAsync("Storyboard");
        }

        public void LoadLevel(string level)
        {
            if (!Application.CanStreamedLevelBeLoaded(level)) return;

            SceneManager.LoadSceneAsync(level);
            
            Time.timeScale = 1f;
        }

        public void MainMenu()
        {
            SceneManager.LoadSceneAsync(0);
        }

        public void Pause(GameObject menu)
        {
            menu.SetActive(true);

            Time.timeScale = 0f;
        }

        public void Resume(GameObject menu)
        {
            menu.SetActive(false);

            Time.timeScale = 1f;
        }
    }
}
