using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int _copieCount;
    [SerializeField] private Transform _container;

    private GameObject[] _pool;

    public void CreatePool(GameObject pooledObject)
    {
        _pool = new GameObject[_copieCount];

        for (int i = 0; i < _copieCount; i++)
        {
            var instance = Instantiate(pooledObject, _container);
            instance.SetActive(false);
            _pool[i] = instance;
        }
    }

    public GameObject GetObject(GameObject pooledObject)
    {
        var instance = _pool.FirstOrDefault(instance => instance.activeSelf == false);

        if (instance == null)
            return Instantiate(pooledObject, _container);
        else
            return instance;
    }
}
