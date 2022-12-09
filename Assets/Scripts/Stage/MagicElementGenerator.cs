using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicElementPool))]
public class MagicElementGenerator : MonoBehaviour
{
    [SerializeField] private List<MagicElement> _elementTypes = new List<MagicElement>();

    private MagicElementPool _pool;

    private void Awake()
    {
        _pool = GetComponent<MagicElementPool>();

        foreach (MagicElement element in _elementTypes)
            _pool.Expand(element);
    }

    public MagicElement GetRandomElement()
    {
        var elementType = _elementTypes[Random.Range(0, _elementTypes.Count)].Type;
        var element = _pool.GetInstance(elementType);
        element.gameObject.SetActive(true);
        element.Deactivated += OnElementDeactivate;

        return element;
    }

    private void OnElementDeactivate(MagicElement element)
    {
        element.Deactivated -= OnElementDeactivate;
        element.transform.SetParent(transform);
        element.gameObject.SetActive(false);
    }
}