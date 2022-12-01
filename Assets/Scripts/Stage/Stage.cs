using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour, ISaveable
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private float _timer;
    [SerializeField] private Player _player;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private VictoryScreen _victoryScreen;

    private int _number;
    private bool _isGameOver;

    public UnityAction<int> TimeChanged;

    public int Number => _number;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _saveLoadSystem.Load();
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            TimeChanged?.Invoke((int)Mathf.Round(_timer));
        }
        else if (_isGameOver == false)
        {
            _isGameOver = true;
            _number++;
            _player.AddStageCoin();
            OnGameOver();
            _victoryScreen.gameObject.SetActive(true);
        }
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

    private void OnHealthChanged(int _health)
    {
        if (_health <= 0)
        {
            OnGameOver();
            _defeatScreen.gameObject.SetActive(true);
        }
    }

    private void OnGameOver()
    {
        _saveLoadSystem.Save();
        Time.timeScale = 0;
    }

    private struct SaveData
    {
        public int number;
    }
}
