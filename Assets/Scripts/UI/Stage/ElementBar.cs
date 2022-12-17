using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElementBar : MonoBehaviour
{
    private const int ElementCount = 5;

    [SerializeField] private MagicElementGenerator _generator;

    private List<MagicElement> _elements = new List<MagicElement>();

    public UnityAction<MagicElement> ElementAdded;

    private void Start()
    {
        AddMissingElements(ElementCount);
    }

    private void AddMissingElements(int elementCount)
    {
        for (int i = 0; i < elementCount; i++)
        {
            MagicElement element = _generator.GetRandomElement();
            element.transform.SetParent(transform);
            _elements.Add(element);
            ElementAdded?.Invoke(element);
        }
    }
}