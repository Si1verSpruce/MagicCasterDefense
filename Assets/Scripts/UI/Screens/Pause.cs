using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : Screen, ISaveable
{
    [SerializeField] private Button _continue;
    [SerializeField] private Toggle _sound;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] private GameObject _screen;
    [SerializeField] private bool _isSoundOnByDefault;

    private void OnEnable()
    {
        RestartSceneButton.onClick.AddListener(RestartScene);
        _continue.onClick.AddListener(CloseScreen);
        _sound.onValueChanged.AddListener(ToggleSoundWithSave);
    }

    private void OnDisable()
    {
        RestartSceneButton.onClick.RemoveListener(RestartScene);
        _continue.onClick.RemoveListener(CloseScreen);
        _sound.onValueChanged.RemoveListener(ToggleSoundWithSave);
    }

    public object SaveState()
    {
        var data = new SaveData() { isSoundOn = _sound.isOn };

        return data;
    }

    public void LoadState(string saveData)
    {
        var data = JsonUtility.FromJson<SaveData>(saveData);
        ToggleSound(data.isSoundOn);
    }

    public void LoadByDefault()
    {
        ToggleSound(_isSoundOnByDefault);
    }

    public void OpenScreen()
    {
        Time.timeScale = 0;
        _screen.SetActive(true);
    }

    private void CloseScreen()
    {
        Time.timeScale = 1;
        _screen.SetActive(false);
    }

    private void ToggleSound(bool isOn)
    {
        AudioListener.pause = !isOn;
        _sound.isOn = isOn;
    }

    private void ToggleSoundWithSave(bool isOn)
    {
        ToggleSound(isOn);
        _saveLoad.Save(this);
    }

    [Serializable]
    private struct SaveData
    {
        public bool isSoundOn;
    }
}