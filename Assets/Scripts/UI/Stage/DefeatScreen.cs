using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _arsenalButton;
    [SerializeField] private Arsenal _arsenal;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartStage);
        _arsenalButton.onClick.AddListener(ActivateArsenalScreen);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        _restartButton.onClick.RemoveListener(RestartStage);
        _arsenalButton.onClick.RemoveListener(ActivateArsenalScreen);
    }

    private void RestartStage()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ActivateArsenalScreen()
    {
        _arsenal.ActivateScreen();
    }
}
