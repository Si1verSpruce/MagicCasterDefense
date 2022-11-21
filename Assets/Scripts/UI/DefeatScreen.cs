using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _shopButton;

    public UnityAction RestartButtonClicked;
    public UnityAction ShopButtonClicked;

    private void OnEnable()
    {
        _restartButton.onClick.AddListener(RestartButtonClicked);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        _restartButton.onClick.RemoveListener(RestartButtonClicked);
    }
}
