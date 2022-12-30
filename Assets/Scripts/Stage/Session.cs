using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Session : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private SaveLoadSystem _saveLoadSystem;
    [SerializeField] private float _time;
    [SerializeField] private Player _player;
    [SerializeField] private MenuScreen _menuScreen;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private int _stageCountBeforeBoss;
    [SerializeField] private GamePause _gamePauseToggle;
    [SerializeField] private SessionRestarter _restarter;

    private int _number;
    private int _bossNumber;
    private bool _isSessionActive;
    private float _currentTime;

    public UnityAction<int> TimeChanged;
    public UnityAction<bool> SessionActivityChanged;

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
        _saveLoadSystem.Load();
        _bossNumber = (_number / _stageCountBeforeBoss + 1) * _stageCountBeforeBoss - 1;
        _menuScreen.OpenScreen();
        _currentTime = _time;
    }

    private void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            TimeChanged?.Invoke((int)Mathf.Round(_currentTime));
        }
        else if (_isSessionActive == true)
        {
            _number++;
            _bossNumber = (_number / _stageCountBeforeBoss + 1) * _stageCountBeforeBoss - 1;
            _player.AddGem();
            OnSessionOver();
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
        _saveLoadSystem.SaveAll();

        _currentTime = _time;
        _isSessionActive = true;
        SessionActivityChanged?.Invoke(_isSessionActive);
    }

    private void OnHealthChanged(int _health)
    {
        if (_health <= 0)
        {
            OnSessionOver();
            _defeatScreen.gameObject.SetActive(true);
        }
    }

    private void OnSessionOver()
    {
        _isSessionActive = false;
        SessionActivityChanged?.Invoke(_isSessionActive);
        _gamePauseToggle.RequestPause(gameObject);
        _saveLoadSystem.SaveAll();
    }

    [Serializable]
    private struct SaveData
    {
        public int number;
    }
}