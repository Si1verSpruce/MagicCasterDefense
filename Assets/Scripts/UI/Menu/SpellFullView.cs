using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellFullView : SpellShortView
{
    [SerializeField] private Image _spellIcon;
    [SerializeField] private TextMeshProUGUI _upgradeDescription;
    [SerializeField] private GameObject _buyGroup;
    [SerializeField] private Button _buy;
    [SerializeField] private TextMeshProUGUI _buyPrice;
    [SerializeField] private GameObject _upgradeGroup;
    [SerializeField] private Button _upgrade;

    public int UpgradePrice => Spell.UpgradePrice;

    public UnityAction<Spell, SpellFullView> BuyButtonClicked;
    public UnityAction<Spell, SpellFullView> UpgradeButtonClicked;
    public UnityAction<int> UpgradePriceChanged;

    public override void Init(Spell spell)
    {
        base.Init(spell);
        _upgradeDescription.text = Spell.UpgradeDescription;
        _spellIcon.sprite = Spell.Icon;
        _buyPrice.text = Spell.BuyPrice.ToString();
        UpgradePriceChanged?.Invoke(Spell.UpgradePrice);

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
        LevelChanged?.Invoke(Spell.Level);
        UpgradePriceChanged?.Invoke(Spell.UpgradePrice);
    }

    private void OnBuyButtonClick()
    {
        BuyButtonClicked?.Invoke(Spell, this);
    }

    private void OnUpgradeButtonClick()
    {
        UpgradeButtonClicked?.Invoke(Spell, this);
    }
}
