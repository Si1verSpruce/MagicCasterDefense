using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _pooledObject;
    [SerializeField] private int _copieCount;
    [SerializeField] private Transform _container;

    private GameObject[] _pool;

    private void Awake()
    {
        _pool = new GameObject[_copieCount];

        for (int i = 0; i < _copieCount; i++)
        {
            var instance = Instantiate(_pooledObject, _container);
            instance.SetActive(false);
            _pool[i] = instance;
        }
    }

    public GameObject GetObject()
    {
        var instance = _pool.FirstOrDefault(instance => instance.activeSelf == false);

        if (instance == null)
            return Instantiate(_pooledObject, _container);
        else
            return instance;
    }
}
