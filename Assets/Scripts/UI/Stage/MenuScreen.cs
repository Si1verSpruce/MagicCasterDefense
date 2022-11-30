using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : Screen
{
    [SerializeField] private Button _play;
    [SerializeField] private Button _back;
    [SerializeField] private Toggle _sound;

    private void OnEnable()
    {
        Time.timeScale = 0;
        RestartSceneButton.onClick.AddListener(RestartScene);
        _play.onClick.AddListener(RestartScene);
        _back.onClick.AddListener(CloseScreen);
        _sound.onValueChanged.AddListener(ToggleSound);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        RestartSceneButton.onClick.RemoveListener(RestartScene);
        _play.onClick.RemoveListener(RestartScene);
        _back.onClick.RemoveListener(CloseScreen);
        _sound.onValueChanged.RemoveListener(ToggleSound);

        _play.gameObject.SetActive(false);
        _back.gameObject.SetActive(true);
    }

    private void CloseScreen()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void ToggleSound(bool isActive)
    {
        AudioListener.volume = Convert.ToInt32(isActive);
    }
}
