using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class MagicElement : Instance
{
    [SerializeField] private MagicElementType _type;

    private bool _isSelected;
    private Outline _outline;

    public ElementType Type => _type.Type;

    public UnityAction<MagicElement, bool> Toggled;
    public UnityAction<MagicElement> Deactivated;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void ToggleSelectionStatus()
    {
        if (_isSelected)
        {
            _isSelected = false;
            _outline.enabled = false;
        }
        else
        {
            _isSelected = true;
            _outline.enabled = true;
        }

        Toggled?.Invoke(this, _isSelected);
    }

    public void Deactivate()
    {
        Deactivated?.Invoke(this);
    }
}
