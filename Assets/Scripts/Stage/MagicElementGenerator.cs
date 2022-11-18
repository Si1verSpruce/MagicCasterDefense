using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicElementGenerator : MonoBehaviour
{
    [SerializeField] private List<MagicElement> _elementTypes = new List<MagicElement>();

    public MagicElement GetRandomElement()
    {
        return Instantiate(_elementTypes[Random.Range(0, _elementTypes.Count)]);
    }

}
