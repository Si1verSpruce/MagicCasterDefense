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
        Instantiate(_spellMaker.CurrentSpell, targetPosition, Quaternion.identity);
        _spellMaker.DestroyElements();
    }
}
