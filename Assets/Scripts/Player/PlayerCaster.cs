using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerElementSelector))]
public class PlayerCaster : MonoBehaviour
{
    private Player _player;
    private PlayerElementSelector _spellMaker;
    private Spell _currentSpell;

    public Spell CurrentSpell => _currentSpell;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _spellMaker = GetComponent<PlayerElementSelector>();
    }

    private void OnEnable()
    {
        _spellMaker.CombinationUpdated += OnCombinationUpdated;
    }

    private void OnDisable()
    {
        _spellMaker.CombinationUpdated -= OnCombinationUpdated;
    }

    private void OnCombinationUpdated(List<MagicElement> elements)
    {
        _currentSpell = _player.GetSpell(elements);
    }

    public void OnCast(Vector3 targetPosition)
    {
        Instantiate(_currentSpell, targetPosition, Quaternion.identity);
        _spellMaker.DestroyCurrentCombination();
    }
}
