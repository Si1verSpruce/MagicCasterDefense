using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpellMaker : MonoBehaviour
{
    [SerializeField] private ElementBar _elementBar;

    private List<MagicElement> _selectedElements = new List<MagicElement>();

    public UnityAction<List<MagicElement>> CombinationUpdated;

    private void OnEnable()
    {
        _elementBar.ElementAdded += OnElementAdd;
    }

    private void OnDisable()
    {
        _elementBar.ElementAdded -= OnElementAdd;
    }

    public void DeactivateCurrentCombination()
    {
        while (_selectedElements.Count > 0)
        {
            _selectedElements[0].ToggleSelectionStatus();
            _selectedElements[0].Deactivate();
        }
    }

    public void UnselectCurrentCombination()
    {
        while (_selectedElements.Count > 0)
            _selectedElements[0].ToggleSelectionStatus();
    }

    private void OnElementAdd(MagicElement element)
    {
        element.Toggled += OnElementSelectionStatusChanged;
        element.Deactivated += OnElementDeactivate;
    }

    private void OnElementDeactivate(MagicElement element)
    {
        element.Toggled -= OnElementSelectionStatusChanged;
        element.Deactivated -= OnElementDeactivate;
    }

    private void OnElementSelectionStatusChanged(MagicElement element, bool isSelected)
    {
        if (isSelected)
            _selectedElements.Add(element);
        else
            _selectedElements.Remove(element);

        CombinationUpdated?.Invoke(_selectedElements);
    }
}
