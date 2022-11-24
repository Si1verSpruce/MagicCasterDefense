using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    [SerializeField] private StageStoredDataHandler _dataHandler;
    [SerializeField] private float _timer;
    [SerializeField] private Player _player;
    [SerializeField] private DefeatScreen _defeatScreen;
    [SerializeField] private VictoryScreen _victoryScreen;
    [SerializeField] private ArsenalScreen _arsenalScreen;

    private int _number;
    private bool _isGameOver;

    public UnityAction<int> TimeChanged;
    public UnityAction GameOver;

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
        _number = _dataHandler.StageNumber;
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
            OnGameOver();
            _victoryScreen.gameObject.SetActive(true);
        }
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
        GameOver?.Invoke();
        Time.timeScale = 0;
    }
}
