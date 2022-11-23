using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : Spawner
{
    [SerializeField] private Vector3 _minOffset;
    [SerializeField] private Vector3 _maxOffset;
    [SerializeField] private int _missleCount;

    private int _misslesLeft;

    private void Awake()
    {
        _misslesLeft = _missleCount;
    }

    protected override void Spawn()
    {
        if (_misslesLeft > 0)
        {
            float positionX = Random.Range(_minOffset.x, _maxOffset.x);
            float positionY = Random.Range(_minOffset.y, _maxOffset.y);
            float positionZ = Random.Range(_minOffset.z, _maxOffset.z);
            SpawnPosition.x = positionX;
            SpawnPosition.y = positionY;
            SpawnPosition.z = transform.position.z + positionZ;

            base.Spawn();

            _misslesLeft--;
        }
    }
}
