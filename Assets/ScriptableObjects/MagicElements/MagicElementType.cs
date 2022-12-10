using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
    Fire,
    Lightning,
    Earth
}

[CreateAssetMenu(fileName = "Magic Element", menuName = "Scriptable Objects/Magic Element")]
public class MagicElementType : ScriptableObject
{
    [SerializeField] private ElementType _type;

    public ElementType Type => _type;
}