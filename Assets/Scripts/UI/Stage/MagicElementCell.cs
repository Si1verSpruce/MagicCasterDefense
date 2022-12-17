using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class MagicElementCell : MonoBehaviour
{
    [SerializeField] private MagicElement[] _elements;
    [SerializeField] private Image _element;

    private Image _image;
    private MagicElement _currentElement;
    private bool _isSelected;

    public UnityAction<MagicElementCell, bool> Toggled;

    public ElementType CurrentElement => _currentElement.Type;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _image.enabled = false;
        SetRandomElement();
    }

    public void ToggleSelection()
    {
        if (_isSelected)
            UpdateSelection(false);
        else
            UpdateSelection(true);

        Toggled?.Invoke(this, _isSelected);
    }

    public void ChangeElement()
    {
        SetRandomElement();
    }

    private void SetRandomElement()
    {
        int number = Random.Range(0, _elements.Length);
        _currentElement = _elements[number];
        _element.sprite = _currentElement.Sprite;
    }

    private void UpdateSelection(bool isSelected)
    {
        _isSelected = isSelected;
        _image.enabled = isSelected;
    }
}
