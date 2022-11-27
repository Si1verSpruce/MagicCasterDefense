using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageCoinsPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinsDisplay;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        OnCoinsChanged(_player.StageCoins);
        _player.StageCoinsChanged += OnCoinsChanged;
    }

    private void OnDisable()
    {
        _player.StageCoinsChanged -= OnCoinsChanged;
    }

    private void OnCoinsChanged(int coins)
    {
        _coinsDisplay.text = coins.ToString();
    }
}
