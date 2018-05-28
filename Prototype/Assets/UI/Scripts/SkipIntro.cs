using Assets.UI.Scripts;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(MenuUI))]
public class SkipIntro : MonoBehaviour
{
    [NotNull] [SerializeField] private string _levelToLoad;
    [SerializeField] private VideoPlayer _videoPlayer;

    private MenuUI _menu;
    private Text _skipText;

    private void Start()
    {
        _menu = GetComponent<MenuUI>();

        _skipText = GetComponentInChildren<Text>();

        _videoPlayer.Play();
    }

    private void LateUpdate()
    {
        if (!_videoPlayer.isPlaying)
        {
            _menu.PlayLevel(_levelToLoad);

            return;
        }

        if (!_skipText.IsActive())
            _skipText.gameObject.SetActive(true);
        else
            _skipText.color = new Color(
                _skipText.color.r,
                _skipText.color.g,
                _skipText.color.b,
                Mathf.Lerp(_skipText.color.a, 255, Time.deltaTime / 5000));

        if (Input.GetKeyDown(KeyCode.Space))
            _menu.PlayLevel(_levelToLoad);
    }
}
