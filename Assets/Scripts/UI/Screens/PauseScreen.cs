using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PauseScreen : RestartScreen
{
    [SerializeField] protected GamePause GamePauseToggle;
    [SerializeField] protected AdBanner Banner;

    public virtual void OpenScreen()
    {
        GamePauseToggle.RequestPause(gameObject);
        Banner.Show();
    }

    public override void CloseScreen()
    {
        GamePauseToggle.RequestPlay(gameObject);
        Banner.Hide();
    }
}
