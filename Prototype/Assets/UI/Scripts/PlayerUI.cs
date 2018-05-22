using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Player.Scripts;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Assets.UI.Scripts;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Slider _oxygen;
    [SerializeField] private PlayerController _player;

    private MenuUI _menu;
    private Gradient _oxygenColor;
    private Image _oxygenImage;
    private ParticleSystem _blowingOxygenParticleSystem;

    private void Awake()
    {
        _oxygenColor = new Gradient
        {
            colorKeys = new[]
            {
                new GradientColorKey(Color.blue, 1f),
                new GradientColorKey(Color.red, .3f),
            },

            alphaKeys = new[]
            {
                new GradientAlphaKey(.5f, 0f),
                new GradientAlphaKey(1f, 1f),
            },

            mode = GradientMode.Blend
        };

        _menu = GetComponentInParent<MenuUI>();

        _oxygen.maxValue = _player.MaxHealth;
        
        _oxygenImage = _oxygen.GetComponentsInChildren<Image>()
            .First(child => string.Compare(child.name, "Oxygen", StringComparison.Ordinal) == 0);

        _blowingOxygenParticleSystem = _oxygen.GetComponentInChildren<ParticleSystem>();
    }
    
    private void OnEnable()
    {
        PlayerController.OnHealthChangedEvent += PlayerOnHealthChangedEvent;
        PlayerController.OnReceiveDamageEvent += PlayerOnReceiveDamageEvent;
    }

    private void OnDisable()
    {
        PlayerController.OnHealthChangedEvent -= PlayerOnHealthChangedEvent;
        PlayerController.OnReceiveDamageEvent -= PlayerOnReceiveDamageEvent;
    }

    private void PlayerOnHealthChangedEvent(float health)
    {
        if (health.CompareTo(0f) <= 0)
        {
            _menu.GameOver();
        }
        else
        {
            _oxygen.value = health;

            _oxygenImage.color = _oxygenColor.Evaluate(_oxygen.value / _oxygen.maxValue);
        }
    }
    
    private void PlayerOnReceiveDamageEvent()
    {
        _blowingOxygenParticleSystem.Play();
    }
}
