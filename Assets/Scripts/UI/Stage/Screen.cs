using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Screen : MonoBehaviour
{
    [SerializeField] private Button _arsenalButton;

    public UnityAction ArsenalButtonClicked;

    protected virtual void OnEnable()
    {
        _arsenalButton.onClick.AddListener(ArsenalButtonClicked);
    }

    protected virtual void OnDisable()
    {
        _arsenalButton.onClick.RemoveListener(ArsenalButtonClicked);
    }
}
