using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private Button _nextStageButton;
    [SerializeField] private Button _shopButton;

    public UnityAction NextStageButtonClicked;
    public UnityAction ShopButtonClicked;

    private void OnEnable()
    {
        _nextStageButton.onClick.AddListener(NextStageButtonClicked);
    }

    private void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        _nextStageButton.onClick.RemoveListener(NextStageButtonClicked);
    }
}
