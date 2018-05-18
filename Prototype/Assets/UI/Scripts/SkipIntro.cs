using System.Collections;
using System.Collections.Generic;
using Assets.UI.Scripts;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(MenuUI))]
public class SkipIntro : MonoBehaviour
{
    private MenuUI _menu;

    [SerializeField]
    private string LevelToLoad;

    [SerializeField]
    private Text SkipText;

    [SerializeField]
    private VideoPlayer VideoPlayer;

    private void Start()
    {
        _menu = GetComponent<MenuUI>();

        _menu.LoadLevel(LevelToLoad);

        VideoPlayer.Play();
    }

    private void LateUpdate()
    {
        if (!VideoPlayer.isPlaying)
        {
            _menu.PlayLevel(LevelToLoad);

            return;
        }

        if (!_menu.IsLevelLoaded(LevelToLoad)) return;

        if (!SkipText.IsActive())
            SkipText.gameObject.SetActive(true);
        else
            SkipText.color = new Color(
                SkipText.color.r,
                SkipText.color.g,
                SkipText.color.b,
                Mathf.Lerp(SkipText.color.a, 255, Time.deltaTime / 5000));

        if (Input.GetKeyDown(KeyCode.Space)) _menu.PlayLevel(LevelToLoad);
    }
}
