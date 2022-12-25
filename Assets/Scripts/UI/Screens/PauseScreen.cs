using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PauseScreen : RestartScreen
{
    [SerializeField] protected GamePause GamePauseToggle;

    public virtual void OpenScreen()
    {
        GamePauseToggle.RequestPause(gameObject);
    }

    public virtual void CloseScreen()
    {
        GamePauseToggle.RequestPlay(gameObject);
    }

    protected override void Deactivate() { }
}
