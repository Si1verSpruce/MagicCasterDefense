using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerSpellMaker))]
public class PlayerCaster : MonoBehaviour
{
    private PlayerSpellMaker _spellMaker;

    private void Awake()
    {
        _spellMaker = GetComponent<PlayerSpellMaker>();
    }

    public void OnCast(Vector3 targetPosition)
    {
        Spell spell = _spellMaker.CurrentSpell;

        if (spell != null)
            Instantiate(spell, targetPosition, Quaternion.identity);
    }
}
