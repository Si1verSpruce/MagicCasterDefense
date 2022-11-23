using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Spell : MonoBehaviour
{
    [SerializeField] private List<MagicElement> _combination = new List<MagicElement>();
    [SerializeField] protected GameObject Missle;

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
