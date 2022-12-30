using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Button _button;

    public UnityAction OnClick;

    public void Init(string message, string[] values, string valueTag)
    {
        var regex = new Regex(Regex.Escape(valueTag));

        for (int i = 0; i < values.Length; i++)
            message = regex.Replace(message, values[i], 1);

        _text.text = message;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnClick?.Invoke();
        gameObject.SetActive(false);
    }
}
