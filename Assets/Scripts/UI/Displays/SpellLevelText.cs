using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class SpellLevelText : MonoBehaviour
{
    [SerializeField] private SpellView _view;

    private TextMeshProUGUI _text;

    public void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        _view.LevelChanged += OnLevelChange;
        OnLevelChange(_view.Level);
    }

    private void OnDisable()
    {
        _view.LevelChanged -= OnLevelChange;
    }

    private void OnLevelChange(int level)
    {
        _text.text = level.ToString();
    }
}
