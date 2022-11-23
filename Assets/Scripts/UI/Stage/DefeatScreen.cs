using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DefeatScreen : Screen
{
    [SerializeField] private Button _restartButton;

    public UnityAction RestartButtonClicked;

    protected override void OnEnable()
    {
        base.OnEnable();
        _restartButton.onClick.AddListener(RestartButtonClicked);
    }

    protected override void OnDisable()
    {
        if (gameObject.scene.isLoaded == false)
            return;

        base.OnDisable();
        _restartButton.onClick.RemoveListener(RestartButtonClicked);
    }
}
