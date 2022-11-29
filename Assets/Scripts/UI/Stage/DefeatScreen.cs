using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Shop _shop;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartStage);
        _shopButton.onClick.AddListener(ActivateShopScreen);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        _restartButton.onClick.RemoveListener(RestartStage);
        _shopButton.onClick.RemoveListener(ActivateShopScreen);
    }

    private void RestartStage()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ActivateShopScreen()
    {
        _shop.ActivateScreen();
    }
}
