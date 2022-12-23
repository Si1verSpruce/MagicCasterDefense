using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Stage : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private float _time;
    [SerializeField] private Player _player;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private int _stageCountBeforeBoss;
    [SerializeField] private GamePauseToggle _gamePauseToggle;
    [SerializeField] private SessionRestarter _restarter;

    private int _number;
    private int _bossNumber;
    private bool _isGameOver;
    private float _currentTime;

    public UnityAction<int> TimeChanged;

    public int Number => _number;
    public int BossNumber => _bossNumber;

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
        _currentTime = _time;
        _saveLoadSystem.Load();
        _bossNumber = (_number / _stageCountBeforeBoss + 1) * _stageCountBeforeBoss - 1;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            TimeChanged?.Invoke((int)Mathf.Round(_currentTime));
        }
        else if (_isGameOver == false)
        {
            _isGameOver = true;
            _number++;
            _bossNumber = (_number / _stageCountBeforeBoss + 1) * _stageCountBeforeBoss - 1;
            _player.AddGem();
            OnGameOver();
            _victoryScreen.gameObject.SetActive(true);
        }
    }

    public object SaveState()
    {
        SaveData data = new SaveData() { number = _number };

        return data;
    }

    public void LoadState(string saveData)
    {
        var savedData = JsonUtility.FromJson<SaveData>(saveData);

        _number = savedData.number;
    }

    public void LoadByDefault() { }

    public void Reset()
    {
        _currentTime = _time;
        _isGameOver = false;
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
        _gamePauseToggle.RequestPause(gameObject);
        _saveLoadSystem.SaveAll();
    }

    [Serializable]
    private struct SaveData
    {
        public int number;
    }
}