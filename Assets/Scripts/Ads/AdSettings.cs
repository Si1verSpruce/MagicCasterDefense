using AppodealStack.Monetization.Api;
using AppodealStack.Monetization.Common;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public enum RewardedAdResult
{
    FailedToLoad,
    ShowFailed,
    Finished
}

public class AdSettings : MonoBehaviour, IRewardedVideoAdListener
{
    [SerializeField] private float _delayBetweenBannerLoads;

    private int _adTypes = AppodealAdType.Interstitial | AppodealAdType.Banner | AppodealAdType.RewardedVideo | AppodealAdType.Mrec;
    private string _appKey = "4f54607ca7caae8faaa00f996cb161d5f7d7fa5314e97797";
    private Coroutine _tryToShowBanner;

    public UnityAction<RewardedAdResult> RewardedAdCompleted;

    private void Start()
    {
        Appodeal.SetRewardedVideoCallbacks(this);
        Appodeal.Initialize(_appKey, _adTypes);
    }

    public void ShowBanner()
    {
        Debug.Log("ShowBanner request");
        if (Appodeal.IsLoaded(AppodealAdType.Banner))
        {
            Debug.Log("Banner loaded");
            Appodeal.Show(AppodealShowStyle.BannerBottom);
        }
        else
        {
            Debug.Log("TryToShowBanner");
            _tryToShowBanner = StartCoroutine(TryToShowBanner());
        }
    }

    public void HideBanner()
    {
        Debug.Log("HideBanner request");
        if (_tryToShowBanner != null)
        {
            Debug.Log("TryToShowBanner coroutine stopped");
            StopCoroutine(_tryToShowBanner);
            _tryToShowBanner = null;
        }
        else
        {
            Debug.Log("Banner hided");
            Appodeal.Hide(AppodealAdType.Banner);
        }
    }

    public void ShowInterstitial()
    {
        Debug.Log("ShowIntertitial request");
        if (Appodeal.IsLoaded(AppodealAdType.Banner))
        {
            Debug.Log("Interstitial loaded");
            Appodeal.Show(AppodealShowStyle.Interstitial);
        }
        else
        {
            Debug.Log("Interstitial doesn't loaded");
        }
    }

    public void ShowRewarded()
    {
        Debug.Log("Rewarded request");
        if (Appodeal.IsLoaded(AppodealAdType.RewardedVideo))
        {
            Debug.Log("Rewarded loaded");
            Appodeal.Show(AppodealShowStyle.RewardedVideo);
        }
    }

    private IEnumerator TryToShowBanner()
    {
        while (Appodeal.IsLoaded(AppodealAdType.Banner) == false)
        {
            yield return new WaitForSecondsRealtime(_delayBetweenBannerLoads);
            Debug.Log("Trying to load banner");
        }

        Debug.Log("Banner loaded");
        Appodeal.Show(AppodealAdType.Banner);
    }

    public void OnRewardedVideoFailedToLoad()
    {
        Debug.Log("Rewarded failed to load");
        RewardedAdCompleted?.Invoke(RewardedAdResult.FailedToLoad);
    }

    public void OnRewardedVideoShowFailed()
    {
        Debug.Log("Rewarded show failed");
        RewardedAdCompleted?.Invoke(RewardedAdResult.ShowFailed);
    }

    public void OnRewardedVideoExpired()
    {
        Debug.Log("Rewarded expired");
        RewardedAdCompleted?.Invoke(RewardedAdResult.ShowFailed);
    }

    public void OnRewardedVideoFinished(double amount, string currency)
    {
        Debug.Log("Rewarded finished");
        RewardedAdCompleted?.Invoke(RewardedAdResult.Finished);
    }

    public void OnRewardedVideoClosed(bool finished)
    {
        Debug.Log("Rewarded closed");
    }
    public void OnRewardedVideoLoaded(bool isPrecache)
    {
        Debug.Log("Rewarded loaded");
    }
    public void OnRewardedVideoShown()
    {
        Debug.Log("Rewarded Shown");
    }
    public void OnRewardedVideoClicked()
    {
        Debug.Log("Rewarded clicked");
    }
}
