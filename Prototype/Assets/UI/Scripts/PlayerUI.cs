using System.Collections;
using System.Collections.Generic;
using Assets.Player.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Slider Oxygen;
    
    private void Update()
    {
        Oxygen.value -= Time.deltaTime;
    }

    private void OnEnable()
    {
        PlayerController.OnHealthChangedEvent += PlayerOnHealthChangedEvent;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChangedEvent -= PlayerOnHealthChangedEvent;
    }

    private void PlayerOnHealthChangedEvent(int health)
    {

#if UNITY_EDITOR
        return;
#endif
    }
}
