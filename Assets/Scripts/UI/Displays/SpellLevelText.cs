using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SpellLevelText : MonoBehaviour
{
    private Arsenal _arsenal;

    private TextMeshProUGUI _text;

    public void Init(Arsenal arsenal, string level)
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = level;
        _arsenal = arsenal;
    }

    private void OnEnable()
    {
        _arsenal.SpellLevelChanged += OnLevelChange;
    }

    private void OnDisable()
    {
        _arsenal.SpellLevelChanged -= OnLevelChange;
    }

    private void OnLevelChange(int level)
    {
        _text.text = level.ToString();
    }
}
