using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;

public class RewardedAd : MonoBehaviour
{
    [SerializeField] private PopupMessage _adNotLoadedWindow;
    [SerializeField] private PopupMessage _adFailedWindow;
    [SerializeField] private PopupMessage _adCompletedWindow;
    [SerializeField] private AdSettings _ads;
    [SerializeField] private GamePause _pause;

    private string _valueTag = "#value#";
    private PopupMessage _activePopup;

    public UnityAction<bool> Rewarded;

    public void SetRewardValues(string[] values)
    {
        _adCompletedWindow.values = values;
    }

    public void Show()
    {
        _pause.RequestPause(gameObject);
        _ads.RewardedAdCompleted += OnRewardedAdCompleted;
        _ads.ShowRewarded();
    }

    private void OnRewardedAdCompleted(RewardedAdResult result)
    {
        switch (result)
        {
            case RewardedAdResult.FailedToLoad:
                ActivatePopupWindow(_adNotLoadedWindow);
                Rewarded?.Invoke(false);
                break;
            case RewardedAdResult.ShowFailed:
                ActivatePopupWindow(_adFailedWindow);
                Rewarded?.Invoke(false);
                break;
            case RewardedAdResult.Finished:
                ActivatePopupWindow(_adCompletedWindow);
                Rewarded?.Invoke(true);
                break;
        }

        _ads.RewardedAdCompleted -= OnRewardedAdCompleted;
    }

    private void ActivatePopupWindow(PopupMessage popup)
    {
        _activePopup = popup;
        popup.window.Init(popup.message, popup.values, _valueTag);
        popup.window.gameObject.SetActive(true);
        popup.window.OnClick += PopupWindowClosed;
    }

    private void PopupWindowClosed()
    {
        _activePopup.window.OnClick -= PopupWindowClosed;
        _activePopup.window.gameObject.SetActive(false);
        _pause.RequestPlay(gameObject);
    }

    [Serializable]
    private struct PopupMessage
    {
        [Header("To insert values in the text use tag: #value#")]
        public string message;
        public PopupWindow window;

        [HideInInspector] public string[] values;
    }
}
