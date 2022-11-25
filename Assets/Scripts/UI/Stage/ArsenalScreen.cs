using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArsenalScreen : MonoBehaviour
{
    [SerializeField] private Button _back;

    private void OnEnable()
    {
        _back.onClick.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        _back.onClick.RemoveListener(Deactivate);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
