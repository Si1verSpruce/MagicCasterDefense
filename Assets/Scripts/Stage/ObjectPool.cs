using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _copieCount;
    [SerializeField] private Transform _container;

    private List<GameObject> _pool = new List<GameObject>();

    public void ExpandPool(GameObject pooledObject)
    {
        for (int i = 0; i < _copieCount; i++)
        {
            var instance = Instantiate(pooledObject, _container);
            _pool.Add(instance);
            instance.SetActive(false);
            _pool[i] = instance;
        }
    }

    public GameObject GetObject<TComponent>(GameObject requestedObject)
    {
        var instance = _pool.FirstOrDefault(instance => instance.activeSelf == false && instance.TryGetComponent<TComponent>(out TComponent component));

        if (instance == null)
            return Instantiate(requestedObject, _container);
        else
            return instance;
    }
}
