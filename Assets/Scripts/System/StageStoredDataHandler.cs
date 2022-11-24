using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStoredDataHandler : MonoBehaviour
{
    [SerializeField] private Stage _stage;
    [SerializeField] private Player _player;

    private SavedDataContainer _container = new SavedDataContainer();

    public int PlayerMoney => _container.PlayerMoney;
    public int StageNumber => _container.StageNumber;

    private void Awake()
    {
        SaveSystem.Init();
        SavedDataContainer container = JsonUtility.FromJson<SavedDataContainer>(SaveSystem.Load());

        if (container != null)
        {
            _container = JsonUtility.FromJson<SavedDataContainer>(SaveSystem.Load());
        }
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
        SaveData();
    }

    private void SaveData()
    {
        _container.PlayerMoney = _player.Money;
        _container.StageNumber = _stage.Number;
        SaveSystem.Save(JsonUtility.ToJson(_container));
    }
}
