using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementBar : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private MagicElementCell _cell;
    [SerializeField] private int _cellCount;
    [SerializeField] private Transform _bar;
    [SerializeField] private int _rewardSessionCountWithFirstCellUnlocked;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] RewardedPopupAd _ad;

    private int _sessionCountWithFirstCellUnlocked;
    private MagicElementCell _firstCell;

    public UnityAction<MagicElementCell> CellAdded;

    public object SaveState()
    {
        return new SaveData { sessionCountWithFirstCellUnlocked = _sessionCountWithFirstCellUnlocked };
    }

    public void LoadState(string jsonData)
    {
        FillBar(_cellCount);
        _sessionCountWithFirstCellUnlocked = JsonUtility.FromJson<SaveData>(jsonData).sessionCountWithFirstCellUnlocked;
    }

    public void LoadByDefault()
    {
        FillBar(_cellCount);
    }

    public void Reset()
    {
        if (_sessionCountWithFirstCellUnlocked == 0)
            _firstCell.Lock();
        else
            TryToUnlockFirstCell();
    }

    private void FillBar(int cellCount)
    {
        for (int i = 0; i < cellCount; i++)
        {
            var cell = Instantiate(_cell, _bar);
            CellAdded?.Invoke(cell);

            if (i == 0)
            {
                cell.Lock();
                _firstCell = cell;
                cell.Clicked += ShowAd;
            }
        }
    }

    private void ShowAd()
    {
        _ad.Rewarded += TryGetReward;
        _ad.Show();
        _ad.SetRewardValues(new string[] { _rewardSessionCountWithFirstCellUnlocked.ToString() });
    }

    private void TryGetReward(bool isRewarded)
    {
        if (isRewarded)
        {
            _sessionCountWithFirstCellUnlocked = _rewardSessionCountWithFirstCellUnlocked;
            TryToUnlockFirstCell();
        }

        _ad.Rewarded -= TryGetReward;
    }

    private void TryToUnlockFirstCell()
    {
        if (_sessionCountWithFirstCellUnlocked > 0)
        {
            _sessionCountWithFirstCellUnlocked--;
            _firstCell.Unlock();
            _saveLoad.Save(this);
        }
    }

    [Serializable]
    private struct SaveData
    {
        public int sessionCountWithFirstCellUnlocked;
    }
}