using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(MagicElement))]
public class ElementToggle : MonoBehaviour, IPointerDownHandler
{
    private MagicElement _element;

    private void Awake()
    {
        _element = GetComponent<MagicElement>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _element.ToggleSelectionStatus();
    }
}
