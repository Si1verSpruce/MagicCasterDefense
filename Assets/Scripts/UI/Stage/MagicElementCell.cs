using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MagicElementCell : MonoBehaviour, IResetOnRestart
{
    [SerializeField] private MagicElement[] _elements;
    [SerializeField] private Image _element;
    [SerializeField] private Image _frame;
    [SerializeField] private Button _lock;

    private MagicElement _currentElement;
    private bool _isSelected;

    public UnityAction<MagicElementCell, bool> Toggled;
    public UnityAction Clicked;

    public ElementType CurrentElement => _currentElement.Type;

    private void Awake()
    {
        Reset();
    }

    private void OnEnable()
    {
        _lock.onClick.AddListener(OnLockClick);
    }

    private void OnDisable()
    {
        _lock.onClick.RemoveListener(OnLockClick);
    }

    private void OnLockClick()
    {
        Clicked?.Invoke();
    }

    public void Lock()
    {
        _lock.gameObject.SetActive(true);
    }

    public void Unlock()
    {
        _lock.gameObject.SetActive(false);
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

    public void Reset()
    {
        _frame.enabled = false;
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
        _frame.enabled = isSelected;
    }
}
