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
        for (int i = 0; i < ElementCount; i++)
        {
            MagicElement element = _generator.GetRandomElement();
            _elements.Add(element);
            element.transform.SetParent(transform);
            ElementAdded?.Invoke(element);
        }
    }
}
