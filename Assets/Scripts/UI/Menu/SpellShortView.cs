using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpellShortView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Transform _combinationContainer;

    protected Spell Spell;

    public int Level => Spell.Level;
    public UnityAction<int> LevelChanged;

    public virtual void Init(Spell spell)
    {
        Spell = spell;

        _label.text = Spell.Label;
        LevelChanged?.Invoke(Spell.Level);
    }
}
