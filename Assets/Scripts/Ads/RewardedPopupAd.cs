using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class RewardedPopupAd : MonoBehaviour
{
    [SerializeField] private PopupMessage _adNotLoaded;
    [SerializeField] private PopupMessage _adFailed;
    [SerializeField] private PopupMessage _adCompleted;
    [SerializeField] private PopupWindow _window;
    [SerializeField] private AdSettings _ads;

    private string _valueTag = "#value#";

    public UnityAction<bool> Rewarded;

    public void SetRewardValues(string[] values)
    {
        _adCompleted.values = values;
    }

    public void Show()
    {
        _ads.RewardedAdCompleted += OnRewardedAdCompleted;
        _ads.ShowRewarded();
    }

    private void OnRewardedAdCompleted(RewardedAdResult result)
    {
        switch (result)
        {
            case RewardedAdResult.FailedToLoad:
                ActivatePopupWindow(_adNotLoaded);
                Rewarded?.Invoke(false);
                break;
            case RewardedAdResult.ShowFailed:
                ActivatePopupWindow(_adFailed);
                Rewarded?.Invoke(false);
                break;
            case RewardedAdResult.Finished:
                ActivatePopupWindow(_adCompleted);
                Rewarded?.Invoke(true);
                break;
        }

        _ads.RewardedAdCompleted -= OnRewardedAdCompleted;
    }

    private void ActivatePopupWindow(PopupMessage popup)
    {
        _window.SetMessage(popup.message, popup.values, _valueTag);
        _window.gameObject.SetActive(true);
        _window.OnClick += PopupWindowClosed;
    }

    private void PopupWindowClosed()
    {
        _window.OnClick -= PopupWindowClosed;
        _window.gameObject.SetActive(false);
    }

    [Serializable]
    private struct PopupMessage
    {
        [Header("To insert values in the text use tag: #value#")]
        public string message;

        [HideInInspector] public string[] values;
    }
}
