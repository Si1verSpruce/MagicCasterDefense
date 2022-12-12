using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicElementPool : InstancePool
{
    [SerializeField] private int _copyCount;

    private Dictionary<ElementType, List<MagicElement>> _elementPool = new Dictionary<ElementType, List<MagicElement>>();

    public Instance[] Expand(Instance pooledInstance)
    {
        var instances = Expand(pooledInstance, _copyCount);
        var instancesList = instances.ToList();
        var elementType = ((MagicElement)instances[0]).Type;

        if (_elementPool.ContainsKey(elementType) == false)
            _elementPool.Add(elementType, new List<MagicElement>());

        _elementPool[elementType] = instancesList.ConvertAll(instance => (MagicElement)instance);

        return instances;
    }

    public MagicElement GetInstance(ElementType elementType)
    {
        var elements = _elementPool[elementType];
        MagicElement element = elements.FirstOrDefault(element => element.gameObject.activeSelf == false);

        return element;
    }
}
