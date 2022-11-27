using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private string _localSaveDirectory;
    [SerializeField] private string _fileName;

    private string _saveDirectory;

    public void Save()
    {
        Time.timeScale = 0;
        Dictionary<string, string> state;

        if (Directory.Exists(_saveDirectory) == false)
            state = new Dictionary<string, string>();
        else
            state = LoadFile();

        SaveState(state);
        SaveFile(state);
        Time.timeScale = 1;
    }

    public void Load()
    {
        Time.timeScale = 0;
        var state = LoadFile();
        LoadState(state);
        Time.timeScale = 1;
    }

    private void SaveFile(object state)
    {
        if (Directory.Exists(_saveDirectory) == false)
            Directory.CreateDirectory(_saveDirectory);

        File.WriteAllText(_saveDirectory + _fileName, JsonConvert.SerializeObject(state));
    }

    private Dictionary<string, string> LoadFile()
    {
        _saveDirectory = Application.dataPath + _localSaveDirectory;

        if (Directory.Exists(_saveDirectory) == false)
            Save();

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_saveDirectory + _fileName));
    }

    private void SaveState(Dictionary<string, string> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            state[saveable.Id] = saveable.SaveState();
    }

    private void LoadState(Dictionary<string, string> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            if (state.TryGetValue(saveable.Id, out string savedState))
                saveable.LoadState(savedState);
    }
}
