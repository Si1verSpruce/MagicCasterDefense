using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementBar : MonoBehaviour
{
    [SerializeField] private MagicElementCell _cell;
    [SerializeField] private int _elementCount;

    public UnityAction<MagicElementCell> CellAdded;

    private void Start()
    {
        for (int i = 0; i < _elementCount; i++)
        {
            var cell = Instantiate(_cell, transform);
            CellAdded?.Invoke(cell);
        }
    }
}