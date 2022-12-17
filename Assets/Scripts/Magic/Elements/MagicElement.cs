using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MagicElement : Instance
{
    [SerializeField] private MagicElementType _type;

    private bool _isSelected;

    public ElementType Type => _type.Type;

    //public UnityAction<MagicElement> Deactivated;

    public void Deactivate()
    {
        //Deactivated?.Invoke(this);
    }
}
