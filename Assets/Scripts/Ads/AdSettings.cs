using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using GoogleMobileAds.Api;
using System.Collections.Generic;
using System;

public enum RewardedAdResult
{
    FailedToLoad,
    ShowFailed,
    Finished
}

public class AdSettings : MonoBehaviour
{
    [SerializeField] private float _delayBetweenBannerLoads;

    private List<BannerView> _bannerViews = new List<BannerView>();
    private string _bannerId = "ca-app-pub-3940256099942544/6300978111";
    private string _interstitialId = "ca-app-pub-3940256099942544/1033173712";
    private string _rewardedId = "ca-app-pub-3940256099942544/5224354917";

    public UnityAction<RewardedAdResult> RewardedAdCompleted;
    public UnityAction AdClosed;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    public void ShowBanner(AdPosition adPosition)
    {
        _bannerViews.Add(new BannerView(_bannerId, AdSize.Banner, adPosition));
        AdRequest request = new AdRequest.Builder().Build();
        _bannerViews[_bannerViews.Count - 1].LoadAd(request);
    }

    public void DestroyBanners()
    {
        foreach (var view in _bannerViews)
            view.Destroy();
    }

    public void TryToShowInterstitial()
    {
        var interstitial = new InterstitialAd(_interstitialId);
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
        interstitial.OnAdClosed += InvokeAdClosed;

        if (interstitial.IsLoaded())
            interstitial.Show();
    }

    public void ShowRewarded()
    {
        var rewarded = new RewardedAd(_rewardedId);
        AdRequest request = new AdRequest.Builder().Build();
        rewarded.LoadAd(request);

        rewarded.OnAdFailedToLoad += OnFailedToLoad;
        rewarded.OnAdFailedToShow += OnFailedToShow;
        rewarded.OnUserEarnedReward += OnUserEarnedReward;

        if (rewarded.IsLoaded())
            rewarded.Show();
    }

    private void OnFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RewardedAdCompleted?.Invoke(RewardedAdResult.FailedToLoad);
    }

    private void OnFailedToShow(object sender, AdErrorEventArgs args)
    {
        RewardedAdCompleted?.Invoke(RewardedAdResult.ShowFailed);
    }

    private void OnUserEarnedReward(object sender, Reward args)
    {
        RewardedAdCompleted?.Invoke(RewardedAdResult.Finished);
    }

    private void InvokeAdClosed(object sender, EventArgs args)
    {
        AdClosed?.Invoke();
    }
}
