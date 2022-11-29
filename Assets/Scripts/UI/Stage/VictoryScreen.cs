using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button _nextStageButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Shop _arsenal;

    private void OnEnable()
    {
        _nextStageButton.onClick.AddListener(StartNextStage);
        _shopButton.onClick.AddListener(ActivateShopScreen);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        _nextStageButton.onClick.RemoveListener(StartNextStage);
        _shopButton.onClick.AddListener(ActivateShopScreen);
    }

    private void StartNextStage()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ActivateShopScreen()
    {
        _arsenal.ActivateScreen();
    }
}
