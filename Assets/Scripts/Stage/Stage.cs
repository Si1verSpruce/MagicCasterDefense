using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Stage : MonoBehaviour
{
    [SerializeField] private int _number;
    [SerializeField] private float _timer;
    [SerializeField] private Player _player;
    [SerializeField] private DefeatScreen _defeatScreen;

    public UnityAction<int> TimeChanged;

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
        _defeatScreen.RestartButtonClicked += RestartGame;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
        _defeatScreen.RestartButtonClicked -= RestartGame;
    }

    private void Update()
    {
        if (_timer > 0)
        {
            _timer -= Time.deltaTime;
            TimeChanged?.Invoke((int)Mathf.Round(_timer));
        }
        else
        {
            Debug.Log("You Win");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        _defeatScreen.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnHealthChanged(int _health)
    {
        if (_health <= 0)
        {
            _defeatScreen.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
