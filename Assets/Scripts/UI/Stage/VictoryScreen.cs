using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VictoryScreen : Screen
{
    [SerializeField] private Button _nextStageButton;

    public UnityAction NextStageButtonClicked;

    protected override void OnEnable()
    {
        base.OnEnable();
        _nextStageButton.onClick.AddListener(NextStageButtonClicked);
    }

    protected override void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        base.OnDisable();
        _nextStageButton.onClick.RemoveListener(NextStageButtonClicked);
    }
}
