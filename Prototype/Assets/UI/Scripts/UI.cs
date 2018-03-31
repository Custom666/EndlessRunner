using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.UI.Scripts
{
    public class UI : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();    
        }

        public void Restart()
        {
            SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);

            Time.timeScale = 1f;
        }
    }
}
