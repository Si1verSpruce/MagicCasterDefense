using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InstancePool))]
public abstract class Spawner : Instance
{
    [SerializeField] protected float Interval;
    [SerializeField] private Instance _spawnedObject;

    private float _timeFromSpawn;
    private InstancePool _pool;

    public Instance SpawnedObject => _spawnedObject;

    private void Awake()
    {
        _pool = GetComponent<InstancePool>();
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
        var instance = _pool.GetInstance(SpawnedObject);

        var position = GetSpawnPosition();
        var rotation = GetSpawnedObjectRotation();
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        OnInstantiated(instance);
    }

    protected virtual void OnInstantiated(Instance instance) { }
}
