using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum ElementType
{
    Fire,
    Lightning,
    Earth
}

[RequireComponent(typeof(Outline))]
public class MagicElement : MonoBehaviour
{
    [SerializeField] private ElementType _type;

    private bool _isSelected;
    private Outline _outline;

    public ElementType Type => _type;

    public UnityAction<MagicElement, bool> SelectionStatusChanged;
    public UnityAction<MagicElement> Destroyed;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void ChangeSelectionStatus()
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

        SelectionStatusChanged?.Invoke(this, _isSelected);
    }

    private void OnDestroy()
    {
        Destroyed?.Invoke(this);
    }
}
