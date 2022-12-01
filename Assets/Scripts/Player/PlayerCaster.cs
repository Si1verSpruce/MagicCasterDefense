using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSpellMaker))]
public class PlayerCaster : MonoBehaviour
{
    [SerializeField] private Vector3 _castPosition;

    private Player _player;
    private PlayerSpellMaker _spellMaker;
    private Spell _currentSpell;

    public Spell CurrentSpell => _currentSpell;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _spellMaker = GetComponent<PlayerSpellMaker>();
    }

    private void OnEnable()
    {
        _spellMaker.CombinationUpdated += OnCombinationUpdated;
    }

    private void OnDisable()
    {
        _spellMaker.CombinationUpdated -= OnCombinationUpdated;
    }

    public void OnCastInput(Vector3 targetPosition)
    {
        if (_currentSpell == null)
        {
            _spellMaker.UnselectCurrentCombination();
        }
        else
        {
            _currentSpell.Cast(targetPosition);
            _spellMaker.DestroyCurrentCombination();
        }
    }

    private void OnCombinationUpdated(List<MagicElement> elements)
    {
        _currentSpell = _player.GetSpell(elements);
    }
}
