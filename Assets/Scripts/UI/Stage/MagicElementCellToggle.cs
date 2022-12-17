using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(MagicElementCell))]
public class MagicElementCellToggle : MonoBehaviour, IPointerDownHandler
{
    private MagicElementCell _cell;

    private void Awake()
    {
        _cell = GetComponent<MagicElementCell>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _cell.ToggleSelection();
    }
}
