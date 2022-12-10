using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class StageNumberText : MonoBehaviour
{
    [SerializeField] protected Stage Stage;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        SetText($"Stage {(Stage.Number + 1)}");
    }

    protected void SetText(string text)
    {
        _text.text = text;
    }
}
