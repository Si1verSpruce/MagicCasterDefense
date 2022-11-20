using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public enum ElementType
{
    Fire,
    Lightning,
    Earth
}

public class MagicElement : MonoBehaviour
{
    [SerializeField] private ElementType _type;

    private bool _isSelected;

    public ElementType Type => _type;

    public UnityAction<MagicElement, bool> Toggled;
    public UnityAction<MagicElement> Destroyed;

    public void ToggleSelectionStatus()
    {
        if (_isSelected)
            _isSelected = false;
        else
            _isSelected = true;

        Toggled?.Invoke(this, _isSelected);
    }

    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) 
            return;

        Destroyed?.Invoke(this);
    }
}
