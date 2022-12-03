using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public abstract class Spawner : MonoBehaviour
{
    [SerializeField] protected float Interval;
    [SerializeField] private GameObject _spawnedObject;

    private float _timeFromSpawn;
    private ObjectPool _pool;

    public GameObject SpawnedObject => _spawnedObject;

    private void Awake()
    {
        _pool = GetComponent<ObjectPool>();
        _pool.Expand(SpawnedObject);
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
        var instance = _pool.GetObject(SpawnedObject);

        var position = GetSpawnPosition();
        var rotation = GetSpawnedObjectRotation();
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.SetActive(true);

        OnObjectSpawned(instance);
    }

    protected virtual void OnObjectSpawned(GameObject instance) { }
}
