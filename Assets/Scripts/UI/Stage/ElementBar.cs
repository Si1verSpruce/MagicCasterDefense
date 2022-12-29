using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElementBar : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private MagicElementCell _cell;
    [SerializeField] private int _cellCount;
    [SerializeField] private Transform _bar;
    [SerializeField] private AdSettings _ads;
    [SerializeField] private int _rewardSessionCountWithFirstCellUnlocked;
    [SerializeField] private Button _adNotLoadedWindow;
    [SerializeField] private Button _adFailedWindow;
    [SerializeField] private Button _adCompletedWindow;
    [SerializeField] private GamePause _pause;
    [SerializeField] private SaveLoadSystem _saveLoad;

    private int _sessionCountWithFirstCellUnlocked;
    private Button _activeWindow;
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
                cell.Clicked += ShowRewardedAd;
            }
        }
    }

    private void ShowRewardedAd()
    {
        _pause.RequestPause(gameObject);
        _ads.RewardedAdCompleted += OnRewardedAdCompleted;
        _ads.ShowRewarded();
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

    private void OnRewardedAdCompleted(RewardedAdResult result)
    {
        switch (result)
        {
            case RewardedAdResult.FailedToLoad:
                ActivatePopupWindow(_adNotLoadedWindow);
                break;
            case RewardedAdResult.ShowFailed:
                ActivatePopupWindow(_adFailedWindow);
                break;
            case RewardedAdResult.Finished:
                ActivatePopupWindow(_adCompletedWindow);
                _sessionCountWithFirstCellUnlocked = _rewardSessionCountWithFirstCellUnlocked;
                TryToUnlockFirstCell();
                break;
        }

        _ads.RewardedAdCompleted -= OnRewardedAdCompleted;
    }

    private void ActivatePopupWindow(Button window)
    {
        _activeWindow = window;
        window.gameObject.SetActive(true);
        window.onClick.AddListener(PopupWindowClosed);
    }

    private void PopupWindowClosed()
    {
        _activeWindow.onClick.RemoveListener(PopupWindowClosed);
        _activeWindow.gameObject.SetActive(false);
        _pause.RequestPlay(gameObject);
    }

    [Serializable]
    private struct SaveData
    {
        public int sessionCountWithFirstCellUnlocked;
    }
}