using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpellMaker : MonoBehaviour
{
    [SerializeField] private ElementBar _elementBar;

    private List<MagicElementCell> _selectedElements = new List<MagicElementCell>();

    public UnityAction<List<MagicElementCell>> CombinationUpdated;

    private void OnEnable()
    {
        //_elementBar.ElementAdded += OnElementAdd;
    }

    private void OnDisable()
    {
        //_elementBar.ElementAdded -= OnElementAdd;
    }

    public void DeactivateCurrentCombination()
    {
        int count = _selectedElements.Count;
        
        for (int i = 0; i < count; i++)
        {
            var element = _selectedElements[0];
            /*element.Deactivate();
            element.ToggleSelectionStatus();
            element.Toggled -= OnElementSelectionStatusChanged;*/
        }
    }

    public void UnselectCurrentCombination()
    {
        /*while (_selectedElements.Count > 0)
            _selectedElements[0].ToggleSelectionStatus();*/
    }

    private void OnElementAdd(MagicElementCell element)
    {
        //element.Toggled += OnElementSelectionStatusChanged;
    }

    private void OnElementSelectionStatusChanged(MagicElementCell element, bool isSelected)
    {
        if (isSelected)
            _selectedElements.Add(element);
        else
            _selectedElements.Remove(element);

        CombinationUpdated?.Invoke(_selectedElements);
    }
}
