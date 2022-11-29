using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StageText : MonoBehaviour, ISaveable
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;

    private int _number;

    private void Awake()
    {
        _saveLoadSystem.Load();
    }

    public string SaveState()
    {
        SaveData data = new SaveData() { number = _number };

        return JsonUtility.ToJson(data);
    }

    public void LoadState(string state)
    {
        var savedData = JsonUtility.FromJson<SaveData>(state);

        _number = savedData.number;
    }

    public new Type GetType()
    {
        return typeof(Stage);
    }

    private struct SaveData
    {
        public int number;
    }
}
