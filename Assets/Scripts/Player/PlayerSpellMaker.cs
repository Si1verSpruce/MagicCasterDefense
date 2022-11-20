using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Player))]
public class PlayerSpellMaker : MonoBehaviour
{
    [SerializeField] private ElementBar _elementBar;

    private List<MagicElement> _selectedElements = new List<MagicElement>();
    private Spell _spell;
    private Player _player;

    public Spell CurrentSpell => _spell;

    public UnityAction<Spell> SpellUpdated;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _elementBar.ElementAdded += OnElementAdded;
    }

    private void OnDisable()
    {
        _elementBar.ElementAdded -= OnElementAdded;
    }

    public void DestroyElements()
    {
        foreach (var element in _selectedElements)
            Destroy(element.gameObject);

        _selectedElements.Clear();
        _spell = null;
        SpellUpdated?.Invoke(_spell);
    }

    private void OnElementAdded(MagicElement element)
    {
        element.Toggled += OnElementSelectionStatusChanged;
        element.Destroyed += OnElementDestroyed;
    }

    private void OnElementDestroyed(MagicElement element)
    {
        element.Toggled -= OnElementSelectionStatusChanged;
        element.Destroyed -= OnElementDestroyed;
    }

    private void OnElementSelectionStatusChanged(MagicElement element, bool isSelected)
    {
        if (isSelected)
            _selectedElements.Add(element);
        else
            _selectedElements.Remove(element);

        _spell = _player.GetSpell(_selectedElements);
        SpellUpdated?.Invoke(_spell);
    }
}
