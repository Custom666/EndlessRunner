using System;
using Assets.Enemies.Scripts;
using Assets.Player.Scripts;
using Assets.Projectiles.Scripts;
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
        private void LateUpdate()
        {
            if (Input.GetButtonDown("Pause")) Pause(gameObject);
        }

        public void Quit()
        {
            Application.Quit();    
        }

        public void Restart()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);

            Time.timeScale = 1f;
        }

        public void MainMenu()
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
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
