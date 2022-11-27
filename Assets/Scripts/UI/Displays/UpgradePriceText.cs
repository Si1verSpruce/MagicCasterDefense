using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpgradePriceText : MonoBehaviour
{
    private Arsenal _arsenal;

    private TextMeshProUGUI _text;

    public void Init(Arsenal arsenal, string price)
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = price;
        _arsenal = arsenal;
    }

    private void OnEnable()
    {
        _arsenal.UpgradePriceChanged += OnPriceChange;
    }

    private void OnDisable()
    {
        _arsenal.UpgradePriceChanged -= OnPriceChange;
    }

    private void OnPriceChange(int price)
    {
        _text.text = price.ToString();
    }
}
