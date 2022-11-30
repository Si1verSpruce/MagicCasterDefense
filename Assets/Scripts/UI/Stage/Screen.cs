using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected Button RestartSceneButton;

    protected void RestartScene()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
