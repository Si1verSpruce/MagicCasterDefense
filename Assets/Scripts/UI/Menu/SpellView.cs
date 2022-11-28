using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellView : MonoBehaviour
{
    [SerializeField] private Image _spellIcon;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private GameObject _buyGroup;
    [SerializeField] private Button _buy;
    [SerializeField] private TextMeshProUGUI _buyPrice;
    [SerializeField] private GameObject _upgradeGroup;
    [SerializeField] private Button _upgrade;
    [SerializeField] private Transform _combinationView;

    private Spell _spell;

    public int Level => _spell.Level;
    public int UpgradePrice => _spell.UpgradePrice;

    public UnityAction<Spell, SpellView> BuyButtonClicked;
    public UnityAction<Spell, SpellView> UpgradeButtonClicked;
    public UnityAction<int> LevelChanged;
    public UnityAction<int> UpgradePriceChanged;

    public void Init(Spell spell)
    {
        _spell = spell;

        _label.text = _spell.Label;
        _spellIcon.sprite = _spell.Icon;
        _buyPrice.text = _spell.BuyPrice.ToString();
        LevelChanged?.Invoke(_spell.Level);
        UpgradePriceChanged?.Invoke(_spell.UpgradePrice);

        foreach (var element in _spell.GetCombination(_combinationView))
            element.GetComponent<Image>().raycastTarget = false;

        if (spell.IsBought)
            ActivateUpgradeGroup();
    }

    private void OnEnable()
    {
        _buy.onClick.AddListener(OnBuyButtonClick);
        _upgrade.onClick.AddListener(OnUpgradeButtonClick);
    }

    public void ActivateUpgradeGroup()
    {
        _buyGroup.SetActive(false);
        _upgradeGroup.SetActive(true);
    }

    public void UpdateUpgradeGroup()
    {
        LevelChanged?.Invoke(_spell.Level);
        UpgradePriceChanged?.Invoke(_spell.UpgradePrice);
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke(_spell, this);
    }

    private void OnUpgradeButtonClick()
    {
        UpgradeButtonClicked?.Invoke(_spell, this);
    }
}
