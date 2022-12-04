using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstancePool : MonoBehaviour
{
    [SerializeField] private int _copieCount;
    [SerializeField] private Transform _container;

    private List<Instance> _pool = new List<Instance>();

    public void Expand(Instance pooledInstance)
    {
        for (int i = 0; i < _copieCount; i++)
        {
            var instance = Instantiate(pooledInstance, _container);
            instance.gameObject.SetActive(false);
            _pool.Add(instance);
        }
    }

    public void ExpandMultiple(IEnumerable pooledInstances)
    {
        foreach (Instance pooledInstance in pooledInstances)
            Expand(pooledInstance);
    }

    public Instance GetObject(Instance requestedInstance)
    {
        var instance = _pool.FirstOrDefault(instance => instance.gameObject.activeSelf == false && 
        requestedInstance.GetType() == instance.GetType());

        if (instance == null)
            return Instantiate(requestedInstance, _container);
        else
            return instance;
    }
}
