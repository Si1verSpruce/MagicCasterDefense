using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCombinationView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private MagicElement[] _combination;
    [SerializeField] private Transform _combinationContainer;

    private Spell _spell;

    public void Init(Spell spell)
    {
        _spell = spell;

        _label.text = _spell.Label;

        foreach (var element in _spell.GetCombination(_combinationContainer))
            element.GetComponent<Image>().raycastTarget = false;
    }
}
