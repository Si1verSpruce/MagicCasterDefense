using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : Spawner
{
    [SerializeField] private Vector3 _minWorldPosition;
    [SerializeField] private Vector3 _maxWorldPosition;
    [SerializeField] private Player _player;
    [SerializeField] private Stage _stage;
    [SerializeField, Min(0)] private float _perStageSpawnFrequencyDivider;

    private void Start()
    {
        Interval /= 1 + _stage.Number * _perStageSpawnFrequencyDivider;
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

    protected override void OnObjectSpawned(GameObject instance)
    {
        instance.GetComponent<Enemy>().Init(_player);
    }
}
