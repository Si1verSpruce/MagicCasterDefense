using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpellMaker : MonoBehaviour
{
    [SerializeField] private ElementBar _elementBar;

    private List<MagicElementCell> _selectedCells = new List<MagicElementCell>();
    private List<ElementType> _selectedElements = new List<ElementType>();

    public UnityAction<List<ElementType>> CombinationUpdated;

    private void OnEnable()
    {
        _elementBar.CellAdded += OnCellAdded;
    }

    private void OnDisable()
    {
        _elementBar.CellAdded -= OnCellAdded;
    }

    public void DeactivateCurrentCombination()
    {
        int count = _selectedCells.Count;
        
        for (int i = 0; i < count; i++)
        {
            var element = _selectedCells[0];
            element.ChangeElement();
            element.ToggleSelection();
        }

        _selectedElements.Clear();
    }

    public void UnselectCurrentCombination()
    {
        while (_selectedCells.Count > 0)
            _selectedCells[0].ToggleSelection();

        _selectedElements.Clear();
    }

    private void OnCellAdded(MagicElementCell cell)
    {
        cell.Toggled += OnCellSelectionChanged;
    }

    private void OnCellSelectionChanged(MagicElementCell cell, bool isSelected)
    {
        if (isSelected)
        {
            _selectedCells.Add(cell);
            _selectedElements.Add(cell.CurrentElement);
        }
        else
        {
            _selectedCells.Remove(cell);
            _selectedElements.Remove(cell.CurrentElement);
        }

        CombinationUpdated?.Invoke(_selectedElements);
    }
}
