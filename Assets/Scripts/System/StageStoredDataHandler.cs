using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStoredDataHandler : MonoBehaviour
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Player _player;

    private SavedDataContainer _container = new SavedDataContainer();

    private void Awake()
    {
        SaveSystem.Init();
        SavedDataContainer container = JsonUtility.FromJson<SavedDataContainer>(SaveSystem.Load());

        if (container == null)
            return;

        _container = JsonUtility.FromJson<SavedDataContainer>(SaveSystem.Load());

        _player.AddMoney(_container.PlayerMoney);
    }

    private void OnEnable()
    {
        _stage.GameOver += OnGameOver;
    }

    private void OnDisable()
    {
        _stage.GameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        _container.PlayerMoney = _player.Money;
        SaveSystem.Save(JsonUtility.ToJson(_container));
    }
}
