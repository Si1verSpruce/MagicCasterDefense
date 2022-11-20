using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerElementSelector : MonoBehaviour
{
    [SerializeField] private ElementBar _elementBar;

    private List<MagicElement> _selectedElements = new List<MagicElement>();

    public UnityAction<List<MagicElement>> CombinationUpdated;

    private void OnEnable()
    {
        _elementBar.ElementAdded += OnElementAdded;
    }

    private void OnDisable()
    {
        _elementBar.ElementAdded -= OnElementAdded;
    }

    public void DestroyCurrentCombination()
    {
        foreach (var element in _selectedElements)
            Destroy(element.gameObject);

        _selectedElements.Clear();
        CombinationUpdated?.Invoke(null);
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

        CombinationUpdated?.Invoke(_selectedElements);
    }
}
