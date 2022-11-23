using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellView : MonoBehaviour
{
    [SerializeField] private Spell _spell;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TextMeshProUGUI _buyPrice;
    [SerializeField] private TextMeshProUGUI _upgradePrice;
    [SerializeField] private GridLayoutGroup _combinationView;

    private void Awake()
    {
        _label.text = _spell.Label;
        _buyPrice.text = _spell.BuyPrice.ToString();
        _upgradePrice.text = _spell.UpgradePrice.ToString();
        MagicElement[] combination = _spell.Combination;

        foreach (var element in combination)
            Instantiate(element, _combinationView.transform);
    }
}
