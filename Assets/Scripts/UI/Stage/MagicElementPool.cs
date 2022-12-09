using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MagicElementPool : InstancePool
{
    private List<MagicElement> _elementPool = new List<MagicElement>();

    public MagicElement GetInstance(ElementType elementType)
    {
        if (_elementPool.Count != Pool.Count)
            _elementPool = Pool.ConvertAll<MagicElement>(instance => (MagicElement)instance);

        MagicElement element = _elementPool.FirstOrDefault(element => element.gameObject.activeSelf == false &&
        elementType == element.Type);

        return element;
    }
}
