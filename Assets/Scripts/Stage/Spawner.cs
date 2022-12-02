using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected float Interval;

    private float _timeFromSpawn;
    private ObjectPool _pool;

    protected void Init(GameObject spawnedObject)
    {
        _pool = GetComponent<ObjectPool>();
        _pool.ExpandPool(spawnedObject);
        _timeFromSpawn = Interval;
    }

    protected abstract Vector3 GetSpawnPosition();

    protected abstract Quaternion GetSpawnedObjectRotation();

    protected void SpawnPerInterval<TComponent>(GameObject spawnedObject)
    {
        if (_timeFromSpawn >= Interval)
        {
            Spawn<TComponent>(spawnedObject);

            _timeFromSpawn = 0;
        }
        else
        {
            _timeFromSpawn += Time.deltaTime;
        }
    }

    protected virtual void Spawn<TComponent>(GameObject spawnedObject)
    {
        var instance = _pool.GetObject<TComponent>(spawnedObject);

        var position = GetSpawnPosition();
        var rotation = GetSpawnedObjectRotation();
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.SetActive(true);

        OnObjectSpawned(instance);
    }

    protected virtual void OnObjectSpawned(GameObject instance) { }
}
