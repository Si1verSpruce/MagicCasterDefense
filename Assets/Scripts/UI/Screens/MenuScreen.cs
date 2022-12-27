using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : PauseScreen
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
}
