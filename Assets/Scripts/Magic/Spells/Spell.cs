using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] private string _label;
    [SerializeField] private Sprite _icon;
    [SerializeField] private int _buyPrice;
    [SerializeField] private int _upgradePrice;
    [SerializeField] private bool _isBought = false;
    [SerializeField] private MagicElement[] _combination;
    [SerializeField] protected GameObject SpawnObject;

    public string Label => _label;
    public Sprite Icon => _icon;
    public int BuyPrice => _buyPrice;
    public int UpgradePrice => _upgradePrice;
    public MagicElement[] Combination => _combination;

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
}
