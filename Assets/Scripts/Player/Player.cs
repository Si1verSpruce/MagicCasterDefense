using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : MonoBehaviour
{
    [SerializeField] private List<Spell> _spells = new List<Spell>();

    public Spell GetSpell(List<MagicElement> combination)
    {
        return _spells.FirstOrDefault(spell => spell.CompareCombinations(combination));
    }
}
