using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SettingsScreen : RestartScreen, ISaveable
{
    [SerializeField] protected GamePauseToggle GamePauseToggle;
    [SerializeField] private Toggle _sound;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] private bool _isSoundOnByDefault;

    private void OnEnable()
    {
        _sound.onValueChanged.AddListener(ToggleSoundWithSave);
    }

    private void OnDisable()
    {
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

    public virtual void OpenScreen()
    {
        GamePauseToggle.RequestPause(gameObject);
    }

    public virtual void CloseScreen()
    {
        GamePauseToggle.RequestPlay(gameObject);
    }

    protected override void Deactivate() { }

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
