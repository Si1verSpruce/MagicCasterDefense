using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected float Interval;

    private float _timeFromSpawn;
    private ObjectPool _pool;

    private void Awake()
    {
        _pool = GetComponent<ObjectPool>();

        _timeFromSpawn = Interval;
    }

    private void Update()
    {
        if (_timeFromSpawn >= Interval)
        {
            Spawn();

            _timeFromSpawn = 0;
        }
        else
        {
            _timeFromSpawn += Time.deltaTime;
        }
    }

    protected abstract Vector3 GetSpawnPosition();

    protected abstract Quaternion GetSpawnedObjectRotation();

    protected virtual void Spawn()
    {
        var instance = _pool.GetObject();

        var position = GetSpawnPosition();
        var rotation = GetSpawnedObjectRotation();
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.SetActive(true);

        OnObjectSpawned(instance);
    }

    protected virtual void OnObjectSpawned(GameObject instance) { }
}
