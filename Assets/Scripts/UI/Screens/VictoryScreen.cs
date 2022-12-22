using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : Screen
{
    [SerializeField] private Button _shopButton;
    [SerializeField] private Shop _shop;

    private void OnEnable()
    {
        RestartSceneButton.onClick.AddListener(RestartSession);
        _shopButton.onClick.AddListener(ActivateShopScreen);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        RestartSceneButton.onClick.RemoveListener(RestartSession);
        _shopButton.onClick.AddListener(ActivateShopScreen);
    }

    private void ActivateShopScreen()
    {
        _shop.ActivateScreen();
    }
}
