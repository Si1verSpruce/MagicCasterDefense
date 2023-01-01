using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PauseScreen : RestartScreen
{
    [SerializeField] protected GamePause GamePauseToggle;
    [SerializeField] private AdSettings _ads;

    public virtual void OpenScreen()
    {
        GamePauseToggle.RequestPause(gameObject);
        _ads.ShowBanner(AdPosition.Top);
        _ads.ShowBanner(AdPosition.Bottom);
    }

    public override void CloseScreen()
    {
        GamePauseToggle.RequestPlay(gameObject);
        _ads.DestroyBanners();
    }
}
