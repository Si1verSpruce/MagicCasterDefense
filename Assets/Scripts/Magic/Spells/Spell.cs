using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    private const int UpgradePricePerLevel = 50;

    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _buyPrice;
    [SerializeField] private bool _isBought = false;
    [SerializeField] private MagicElement[] _combination;
    [SerializeField] protected GameObject SpawnObject;

    private int _upgradePrice;
    private int _level = 1;

    public string Label => _label;
    public Sprite Icon => _icon;
    public int BuyPrice => _buyPrice;
    public int UpgradePrice => _upgradePrice;
    public bool IsBought => _isBought;
    public int Level => _level;

    public void Init(bool isBought, int level)
    {
        _isBought = isBought;
        _level = level;

        _upgradePrice = level * UpgradePricePerLevel;
    }

    public bool CompareCombinations(List<MagicElement> combination)
    {
        if (combination == null)
            return false;

        if (_combination.Count() != combination.Count())
            return false;

        var spellCombination = _combination.OrderBy(element => element.Type).ToArray();
        var receivedCombination = combination.OrderBy(element => element.Type).ToArray();

        for (int i = 0; i < spellCombination.Count(); i++)
            if (spellCombination[i].Type != receivedCombination[i].Type)
                return false;

        return true;
    }

    public abstract void Cast(Vector3 position);

    public List<MagicElement> GetCombination(Transform container)
    {
        List<MagicElement> elements = new List<MagicElement>();

        foreach (var element in _combination)
        {
            var newElement = Instantiate(element, container);
            newElement.GetComponent<Image>().raycastTarget = false;
        }

        return elements;
    }

    public void Buy()
    {
        _isBought = true;
    }
}
