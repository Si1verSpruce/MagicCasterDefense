using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : Screen
{
    [SerializeField] private Button _continue;
    [SerializeField] private Toggle _sound;
    [SerializeField] private SaveLoadSystem _saveLoad;

    private void OnEnable()
    {
        _saveLoad.Save();
        Time.timeScale = 0;
        RestartSceneButton.onClick.AddListener(RestartScene);
        _continue.onClick.AddListener(CloseScreen);
        _sound.onValueChanged.AddListener(ToggleSound);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        RestartSceneButton.onClick.RemoveListener(RestartScene);
        _continue.onClick.RemoveListener(CloseScreen);
        _sound.onValueChanged.RemoveListener(ToggleSound);
    }

    private void Start()
    {
        ToggleSound(_sound.isOn);
    }

    private void CloseScreen()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    private void ToggleSound(bool isOn)
    {
        AudioListener.pause = !isOn;
    }
}
