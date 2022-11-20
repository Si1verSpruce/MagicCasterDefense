using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]
public class CastSpellPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PlayerElementSelector _spellMaker;

    private bool _spellMade;
    private Outline _outline;

    public UnityAction PointerDowned;

    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_spellMade)
            PointerDowned?.Invoke();
    }

    private void OnSpellMade(Spell spell)
    {
        _spellMade = spell != null;

        if (_spellMade)
            _outline.enabled = true;
        else
            _outline.enabled = false;
    }
}
