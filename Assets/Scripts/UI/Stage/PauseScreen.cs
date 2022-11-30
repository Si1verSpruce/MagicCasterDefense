using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : Screen, ISaveable
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

    public string SaveState()
    {
        var data = new SaveData() { isSoundOn = _sound.isOn };

        return JsonUtility.ToJson(data);
    }

    public void LoadState(string jsonString)
    {
        var data = JsonUtility.FromJson<SaveData>(jsonString);
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

    private struct SaveData
    {
        public bool isSoundOn;
    }
}
