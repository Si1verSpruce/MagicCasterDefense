using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [Header("To insert values in the text use tag: #value#")]
    [SerializeField] private string _message;

    public UnityAction OnClick;

    private void Awake()
    {
        {s}
        Debug.Log(_message);
    }
}
