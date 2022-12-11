using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProbabilityChange
{
    Decrease,
    Constant,
    Increase
}

[RequireComponent(typeof(InstancePool))]
public abstract class Spawner : Instance
{
    [SerializeField] protected float Interval;
    [SerializeField] protected SpawnedObject[] SpawnedObjects;

    private float _timeFromSpawn;
    private InstancePool _pool;

    private void Awake()
    {
        _pool = GetComponent<InstancePool>();

        foreach (var spawnedObject in SpawnedObjects)
            _pool.Expand(spawnedObject.Instance, spawnedObject.CopyCount);

        _timeFromSpawn = Interval;
    }

    private void Update()
    {
        SpawnPerInterval();
    }

    protected abstract Instance GetSpawnedInstance();

    protected abstract Vector3 GetSpawnPosition();

    protected abstract Quaternion GetSpawnedObjectRotation();

    protected virtual void SpawnPerInterval()
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

    protected virtual void Spawn()
    {
        var instance = _pool.GetInstance(GetSpawnedInstance());
        var position = GetSpawnPosition();
        var rotation = GetSpawnedObjectRotation();
        instance.transform.position = position;
        instance.transform.rotation = rotation;
        instance.gameObject.SetActive(true);

        OnInstantiated(instance);
    }

    protected virtual void OnInstantiated(Instance instance) { }

    [System.Serializable]
    public class SpawnedObject
    {
        public Instance Instance;
        public int CopyCount;
        [Range(0, 1)] public float BaseChance;
        public ProbabilityChange ChanceChange;
        [Range(0, 1)] public float ChancePerStageModifier;
    }
}
