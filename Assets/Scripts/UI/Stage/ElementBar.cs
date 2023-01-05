using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementBar : MonoBehaviour, ISaveable, IResetOnRestart
{
    [SerializeField] private MagicElementCell[] _cells;
    [SerializeField] private int _rewardSessionCountWithFirstCellUnlocked;
    [SerializeField] private SaveLoadSystem _saveLoad;
    [SerializeField] RewardedPopupAd _ad;
    [SerializeField] private YesNoPopupWindow _confirmationWindow;
    [SerializeField] private string _confirmationWindowMessage;

    private int _sessionCountWithFirstCellUnlocked;
    private MagicElementCell _firstCell;

    public UnityAction<MagicElementCell> CellAdded;

    public object SaveState()
    {
        return new SaveData { sessionCountWithFirstCellUnlocked = _sessionCountWithFirstCellUnlocked };
    }

    public void LoadState(string jsonData)
    {
        Init();
        _sessionCountWithFirstCellUnlocked = JsonUtility.FromJson<SaveData>(jsonData).sessionCountWithFirstCellUnlocked;
    }

    public void LoadByDefault()
    {
        Init();
    }

    private void OnEnable()
    {
        _confirmationWindow.ConfirmClick += ShowAd;
        _confirmationWindow.RejectClick += OnRejectClick;
    }

    private void OnDisable()
    {
        _confirmationWindow.ConfirmClick -= ShowAd;
        _confirmationWindow.RejectClick -= OnRejectClick;
        _firstCell.Clicked -= RequestAd;
    }

    public void Reset()
    {
        if (_sessionCountWithFirstCellUnlocked == 0)
            _firstCell.Lock();
        else
            TryToUnlockFirstCell();
    }

    private void Init()
    {
        for (int i = 0; i < _cells.Length; i++)
        {
            CellAdded?.Invoke(_cells[i]);

            if (i == 0)
            {
                _cells[i].Lock();
                _firstCell = _cells[i];
                _cells[i].Clicked += RequestAd;
            }
        }
    }

    private void OnRejectClick()
    {
        Time.timeScale = 1;
    }

    private void RequestAd()
    {
        _confirmationWindow.gameObject.SetActive(true);
        _confirmationWindow.SetMessage(_confirmationWindowMessage, 
            new string[] { _rewardSessionCountWithFirstCellUnlocked.ToString() }, PopupWindowParameters.ValueTag);
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