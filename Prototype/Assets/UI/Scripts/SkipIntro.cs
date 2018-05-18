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

        SkipText.gameObject.SetActive(true);
        
        if(Input.GetKeyDown(KeyCode.Space)) _menu.PlayLevel(LevelToLoad);
    }
}
