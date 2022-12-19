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

    public void SaveAll()
    {
        Dictionary<string, string> state;

        if (File.Exists(_saveDirectory + _fileName) == false)
            state = new Dictionary<string, string>();
        else
            state = LoadFile();

        SaveStates(state);
        SaveFile(state);
    }
    
    public void Save(ISaveable saveableObject)
    {
        Dictionary<string, string> state;

        if (File.Exists(_saveDirectory + _fileName) == false)
            state = new Dictionary<string, string>();
        else
            state = LoadFile();

        SaveState(state, saveableObject);
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
            return null;

        return JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText(_saveDirectory + _fileName));
    }

    private void SaveStates(Dictionary<string, string> state)
    {
        foreach (var saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            state[saveable.GetType().ToString()] = saveable.SaveState();
    }

    private void SaveState(Dictionary<string, string> state, ISaveable saveable)
    {
        state[saveable.GetType().ToString()] = saveable.SaveState();
    }

    private void LoadState(Dictionary<string, string> state)
    {
        foreach (var saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
        {
            if (state != null)
            {
                if (state.TryGetValue(saveable.GetType().ToString(), out string savedState))
                    saveable.LoadState(savedState);
                else
                    saveable.LoadByDefault();
            }
            else
            {
                saveable.LoadByDefault();
            }
        }
    }
}
