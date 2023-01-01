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

    private BannerView _bottomBanner;
    private BannerView _topBanner;
    private string _bannerId = "ca-app-pub-3940256099942544/6300978111";
    private InterstitialAd _interstitial;
    private string _interstitialId = "ca-app-pub-3940256099942544/1033173712";
    private RewardedAd _rewarded;
    private string _rewardedId = "ca-app-pub-3940256099942544/5224354917";

    public UnityAction<RewardedAdResult> RewardedAdCompleted;
    public UnityAction AdClosed;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { });

        LoadBanner(ref _bottomBanner);
        LoadBanner(ref _topBanner);
        LoadInterstitial();
        LoadRewarded();
    }

    public void ShowBanners()
    {
        ShowBanner(_bottomBanner, AdPosition.Bottom);
        ShowBanner(_topBanner, AdPosition.Top);
    }

    public void HideBanners()
    {
        _bottomBanner.Hide();
        _topBanner.Hide();
    }

    public void TryToShowInterstitial()
    {
        if (_interstitial.IsLoaded())
        {
            _interstitial.Show();
            _interstitial.OnAdClosed += OnInterstitialClosed;
        }
    }

    public void ShowRewarded()
    {
        if (_rewarded.IsLoaded())
        {
            _rewarded.Show();
            _rewarded.OnAdFailedToLoad += OnFailedToLoad;
            _rewarded.OnAdFailedToShow += OnFailedToShow;
            _rewarded.OnUserEarnedReward += OnUserEarnedReward;
        }
    }

    private void ShowBanner(BannerView view, AdPosition position)
    {
        view.Show();
        view.SetPosition(position);
    }

    private void LoadBanner(ref BannerView view)
    {
        view = new BannerView(_bannerId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        view.LoadAd(request);
        view.Hide();
    }

    private void LoadInterstitial()
    {
        _interstitial = new InterstitialAd(_interstitialId);
        AdRequest request = new AdRequest.Builder().Build();
        _interstitial.LoadAd(request);
    }

    private void LoadRewarded()
    {
        _rewarded = new RewardedAd(_rewardedId);
        AdRequest request = new AdRequest.Builder().Build();
        _rewarded.LoadAd(request);
    }

    private void OnInterstitialClosed(object sender, EventArgs args)
    {
        AdClosed?.Invoke();
        _interstitial.OnAdClosed -= OnInterstitialClosed;

        LoadInterstitial();
    }

    private void OnFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RewardedAdCompleted?.Invoke(RewardedAdResult.FailedToLoad);
        OnRewardedCompleted();
    }

    private void OnFailedToShow(object sender, AdErrorEventArgs args)
    {
        RewardedAdCompleted?.Invoke(RewardedAdResult.ShowFailed);
        OnRewardedCompleted();
    }

    private void OnUserEarnedReward(object sender, Reward args)
    {
        RewardedAdCompleted?.Invoke(RewardedAdResult.Finished);
        OnRewardedCompleted();
    }

    private void OnRewardedCompleted()
    {
        _rewarded.OnAdFailedToLoad -= OnFailedToLoad;
        _rewarded.OnAdFailedToShow -= OnFailedToShow;
        _rewarded.OnUserEarnedReward -= OnUserEarnedReward;

        LoadRewarded();
    }
}
