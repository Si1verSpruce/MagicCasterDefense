using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : SettingsScreen
{
    [SerializeField] private Shop _shop;

    public void ActivateShopScreen()
    {
        _shop.ActivateScreen();
    }

    public override void OpenScreen()
    {
        base.OpenScreen();
        gameObject.SetActive(true);
    }

    public override void CloseScreen()
    {
        base.CloseScreen();
        gameObject.SetActive(false);
    }

    protected override void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
