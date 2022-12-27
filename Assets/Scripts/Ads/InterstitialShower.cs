using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterstitialShower : MonoBehaviour
{
    [SerializeField] private float _sessionsTimeToShow;
    [SerializeField] private float _gameStartedTime;
    [SerializeField] private Stage _stage;
    [SerializeField] private AdInterstitial _interstitial;

    private float _time;
    private bool _isSessionActive;

    private void Awake()
    {
        _time = _gameStartedTime;
    }

    private void OnEnable()
    {
        _stage.SessionActivityChanged += OnSessionActivityChanged;
    }

    private void OnDisable()
    {
        _stage.SessionActivityChanged -= OnSessionActivityChanged;
    }

    private void Update()
    {
        if (_isSessionActive)
        {
            _time += Time.deltaTime;
        }
    }

    private void OnSessionActivityChanged(bool isSessionActive)
    {
        _isSessionActive = isSessionActive;

        if (isSessionActive == false && _time == _sessionsTimeToShow)
        {
            _time = 0;
            _interstitial.Show();
        }
    }
}
