using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InstancePool : MonoBehaviour
{
    [SerializeField] private Transform _container;

    protected List<Instance> Pool = new List<Instance>();

    public Instance[] Expand(Instance pooledInstance, int copyCount)   
    {
        Instance[] instances = new Instance[copyCount];

        for (int i = 0; i < copyCount; i++)
        {
            var instance = Instantiate(pooledInstance, _container);
            instance.gameObject.SetActive(false);
            Pool.Add(instance);
            instances[i] = instance;
        }

        return instances;
    }

    public Instance GetInstance(Instance requestedInstance)
    {
        var instance = Pool.FirstOrDefault(instance => instance.gameObject.activeSelf == false &&
        requestedInstance.GetType() == instance.GetType());

        if (instance == null)
            return Instantiate(requestedInstance, _container);
        else
            return instance;
    }
}
