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
    [SerializeField] private TextMeshProUGUI _upgradePrice;
    [SerializeField] private Transform _combinationView;

    private Spell _spell;

    public UnityAction<Spell, SpellView> BuyButtonClicked;
    public UnityAction UpgradeButtonClicked;
    
    public void Init(Spell spell)
    {
        _spell = spell;

        _label.text = _spell.Label;
        _spellIcon.sprite = _spell.Icon;
        _buyPrice.text = _spell.BuyPrice.ToString();
        _upgradePrice.text = _spell.UpgradePrice.ToString();

        foreach (var element in _spell.GetCombination(_combinationView))
            element.GetComponent<Image>().raycastTarget = false;

        UpdateState();
    }

    private void OnEnable()
    {
        _buy.onClick.AddListener(OnBuyButtonClicked);
        _upgrade.onClick.AddListener(UpgradeButtonClicked);
    }

    public void UpdateState()
    {
        if (_spell.IsBought)
        {
            _buyGroup.SetActive(false);
            _upgradeGroup.SetActive(true);
        }
        else
        {
            _buyGroup.SetActive(true);
            _upgradeGroup.SetActive(false);
        }
    }

    private void OnBuyButtonClicked()
    {
        BuyButtonClicked?.Invoke(_spell, this);
    }
}
