using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(MagicElement))]
[RequireComponent(typeof(Outline))]
public class ElementToggle : MonoBehaviour, IPointerDownHandler
{
    private MagicElement _element;
    private Outline _outline;

    private void Awake()
    {
        _element = GetComponent<MagicElement>();
        _outline = GetComponent<Outline>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _element.ToggleSelectionStatus();

        if (_outline.enabled)
            _outline.enabled = false;
        else
            _outline.enabled = true;
    }
}
