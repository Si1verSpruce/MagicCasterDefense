using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : PauseScreen
{
    [SerializeField] private GameObject _screen;

    public override void OpenScreen()
    {
        base.OpenScreen();
        _screen.SetActive(true);
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
        _screen.SetActive(false);
    }

    protected override void Deactivate()
    {
        _screen.SetActive(false);
    }
}
