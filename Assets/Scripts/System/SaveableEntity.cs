using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] private string _id;

    public string Id => _id;

    [ContextMenu("Generate Id")]
    private void GenerateId()
    {
        _id = Guid.NewGuid().ToString();
    }

    public string SaveState()
    {
        var state = new Dictionary<string, string>();

        foreach (var saveable in GetComponents<ISaveable>())
            state[saveable.GetType().ToString()] = saveable.SaveState();

        return JsonConvert.SerializeObject(state);
    }
    
    public void LoadState(string state)
    {
        Dictionary<string, string> stateDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(state);

        foreach (var saveable in GetComponents<ISaveable>())
        {
            var typeName = saveable.GetType().ToString();

            if (stateDictionary.TryGetValue(typeName, out string savedState))
                saveable.LoadState(savedState);
        }
    }
}
