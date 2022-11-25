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
        var state = LoadFile();
        SaveState(state);
        SaveFile(state);
    }

    public void Load()
    {
        var state = LoadFile();
        LoadState(state);
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
            return new Dictionary<string, string>();

        if (File.Exists(_saveDirectory + _fileName) == false)
            return new Dictionary<string, string>();

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
