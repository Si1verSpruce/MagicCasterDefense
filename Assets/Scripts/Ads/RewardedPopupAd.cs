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
        _ads.RewardedAdStateChanged += OnRewardedAdCompleted;
        _ads.ShowRewarded();
    }

    private void OnRewardedAdCompleted(RewardedAdState result)
    {
        switch (result)
        {
            case RewardedAdState.FailedToLoad:
                Rewarded?.Invoke(false);
                StartCoroutine(ActivatePopupWindowEndFrame(_adNotLoaded));
                break;
            case RewardedAdState.ShowFailed:
                Rewarded?.Invoke(false);
                StartCoroutine(ActivatePopupWindowEndFrame(_adFailed));
                break;
            case RewardedAdState.Finished:
                Rewarded?.Invoke(true);
                StartCoroutine(ActivatePopupWindowEndFrame(_adCompleted));
                break;
        }

        _ads.RewardedAdStateChanged -= OnRewardedAdCompleted;
    }

    private IEnumerator ActivatePopupWindowEndFrame(PopupMessage popup)
    {
        yield return new WaitForEndOfFrame();

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
