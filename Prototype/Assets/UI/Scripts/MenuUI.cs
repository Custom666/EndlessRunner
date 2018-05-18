using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly IDictionary<string, AsyncOperation> _loadedLevels = new Dictionary<string, AsyncOperation>();
        
        private void LateUpdate()
        {
            if (Input.GetButtonDown("Pause")) Pause(gameObject);
        }

        public void Quit()
        {
            Application.Quit();    
        }
        
        public void LoadLevel(string name)
        {
            if (!Application.CanStreamedLevelBeLoaded(name))
                throw new FileNotFoundException(string.Format("Scene with name {0} do not exist"), name);

            var level = SceneManager.LoadSceneAsync(name);

            level.allowSceneActivation = false;

            _loadedLevels.Add(name, level);
        }

        public void PlayLevel(string name)
        {
            AsyncOperation level;

            if (!_loadedLevels.TryGetValue(name, out level))
            {
                LoadLevel(name);

                PlayLevel(name);
            }
            else level.allowSceneActivation = true;
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

        public bool IsLevelLoaded(string name)
        {
            AsyncOperation level;

            return _loadedLevels.TryGetValue(name, out level) && level.progress.CompareTo(0.9f) == 0;
        }
    }
}
