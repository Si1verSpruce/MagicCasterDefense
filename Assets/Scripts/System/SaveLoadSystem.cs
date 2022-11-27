using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private string _localSaveDirectory;
    [SerializeField] private string _fileName;

    private string _saveDirectory;

    public void Save()
    {
        Dictionary<string,string> state;

        if (File.Exists(_saveDirectory + _fileName) == false)
            state = new Dictionary<string, string>();
        else
            state = LoadFile();

        SaveState(state);
        SaveFile(state);
    }

    public void Load()
    {
        _saveDirectory = Application.dataPath + _localSaveDirectory;

        var state = LoadFile();
        LoadState(state);
    }

    private void SaveFile(Dictionary<string, string> state)
    {
        if (Directory.Exists(_saveDirectory) == false)
            Directory.CreateDirectory(_saveDirectory);

        File.WriteAllText(_saveDirectory + _fileName, JsonConvert.SerializeObject(state));
    }

    private Dictionary<string, string> LoadFile()
    {
        if (File.Exists(_saveDirectory + _fileName) == false)
            Save();

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_saveDirectory + _fileName));
    }

    private void SaveState(Dictionary<string, string> state)
    {
        foreach (var saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            state[saveable.GetType().ToString()] = saveable.SaveState();
    }

    private void LoadState(Dictionary<string, string> state)
    {
        foreach (var saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            if (state.TryGetValue(saveable.GetType().ToString(), out string savedState))
                saveable.LoadState(savedState);
    }
}
