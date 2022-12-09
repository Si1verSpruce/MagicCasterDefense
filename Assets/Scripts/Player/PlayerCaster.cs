using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerSpellMaker))]
[RequireComponent(typeof(InstancePool))]
public class PlayerCaster : MonoBehaviour
{
    private Player _player;
    private PlayerSpellMaker _spellMaker;
    private Spell _currentSpell;
    private InstancePool _pool;

    public Spell CurrentSpell => _currentSpell;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _spellMaker = GetComponent<PlayerSpellMaker>();
        _pool = GetComponent<InstancePool>();
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
            _currentSpell.Cast(_pool.GetInstance(_currentSpell.InstanceToCreate), targetPosition);
            _spellMaker.DeactivateCurrentCombination();
        }
    }

    private void OnCombinationUpdated(List<MagicElement> elements)
    {
        _currentSpell = _player.GetSpell(elements);
    }
}
