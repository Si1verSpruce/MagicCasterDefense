using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private string _saveDirectory;
    [SerializeField] private string _fileName;

    private void Awake()
    {
        _saveDirectory = Application.dataPath + _saveDirectory;
    }

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

        File.WriteAllText(_saveDirectory + _fileName, JsonUtility.ToJson(state));
    }

    private Dictionary<string, object> LoadFile()
    {
        if (Directory.Exists(_saveDirectory) == false)
            return new Dictionary<string, object>();

        if (File.Exists(_saveDirectory + _fileName) == false)
            return new Dictionary<string, object>();

        return JsonUtility.FromJson<Dictionary<string, object>>(File.ReadAllText(_saveDirectory + _fileName));
    }

    private void SaveState(Dictionary<string, object> state)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            state[saveable.Id] = saveable.SaveState();
    }

    private void LoadState(Dictionary<string, object> states)
    {
        foreach (var saveable in FindObjectsOfType<SaveableEntity>())
            if (states.TryGetValue(saveable.Id, out object savedState))
                saveable.LoadState(savedState);
    }
}
