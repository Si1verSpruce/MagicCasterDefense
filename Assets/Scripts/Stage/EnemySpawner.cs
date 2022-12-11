using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Instance _boss;
    [SerializeField] private Vector3 _minWorldPosition;
    [SerializeField] private Vector3 _maxWorldPosition;
    [SerializeField] private Player _player;
    [SerializeField] private Stage _stage;
    [SerializeField, Min(0)] private float _perStageSpawnFrequencyDivider;

    private Dictionary<Instance, Vector2> _spawnNumbersByInstances = new Dictionary<Instance, Vector2>();

    private void Start()
    {
        Interval /= 1 + _stage.Number * _perStageSpawnFrequencyDivider;

        float lastMaxNumber = 0;

        foreach (var spawnedObject in SpawnedObjects)
        {
            _spawnNumbersByInstances[spawnedObject.Instance] = new Vector2(lastMaxNumber, lastMaxNumber + spawnedObject.BaseChance);
            lastMaxNumber = _spawnNumbersByInstances[spawnedObject.Instance].y;
        }
/*
        foreach (var maxNumber in _spawnNumbersByInstances)
            Debug.Log(maxNumber);*/
    }

    protected override Instance GetSpawnedInstance()
    {
        float value = Random.Range(0f, 1f);
        Debug.Log(value);

        foreach (var spawnNumbers in _spawnNumbersByInstances)
            if (value >= spawnNumbers.Value.x && value <= spawnNumbers.Value.y)
                return spawnNumbers.Key;

        return null;
    }

    protected override Quaternion GetSpawnedObjectRotation()
    {
        return transform.rotation;
    }

    protected override Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(_minWorldPosition.x, _maxWorldPosition.x);
        float positionY = Random.Range(_minWorldPosition.y, _maxWorldPosition.y);
        float positionZ = Random.Range(_minWorldPosition.z, _maxWorldPosition.z);

        return new Vector3(positionX, positionY, positionZ);
    }

    protected override void OnInstantiated(Instance instance)
    {
        instance.GetComponent<Enemy>().Init(_player);
    }
}
