using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstancePool : MonoBehaviour
{
    [SerializeField] private Transform _container;

    protected Dictionary<Instance, List<Instance>> Pool = new Dictionary<Instance, List<Instance>>();

    public Instance[] Expand(Instance pooledInstance, int copyCount)   
    {
        Instance[] instances = new Instance[copyCount];

        if (Pool.ContainsKey(pooledInstance) == false)
            Pool.Add(pooledInstance, new List<Instance>());

        for (int i = 0; i < copyCount; i++)
        {
            var instance = Instantiate(pooledInstance, _container);
            instance.gameObject.SetActive(false);
            Pool[pooledInstance].Add(instance);
            instances[i] = instance;
        }

        return instances;
    }

    public Instance GetInstance(Instance requestedInstance)
    {
        var instances = Pool[requestedInstance];
        var instance = instances.FirstOrDefault(instance => instance.gameObject.activeSelf == false);

        if (instance == null)
            return Instantiate(requestedInstance, _container);
        else
            return instance;
    }

    public Instance[] GetInstances(Instance requestedInstance)
    {
        return Pool[requestedInstance].ToArray();
    }
}
