using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleSpawner : Spawner
{
    [SerializeField] private Vector3 _minOffset;
    [SerializeField] private Vector3 _maxOffset;
    [SerializeField] private int _missleCount;

    private int _misslesLeft;

    private void Start()
    {
        _misslesLeft = _missleCount;
    }

    protected override void Spawn()
    {
        if (_misslesLeft > 0)
        {
            base.Spawn();

            _misslesLeft--;
        }
    }

    protected override Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(_minOffset.x, _maxOffset.x);
        float positionY = Random.Range(_minOffset.y, _maxOffset.y);
        float positionZ = transform.position.z + Random.Range(_minOffset.z, _maxOffset.z);

        return new Vector3(positionX, positionY, positionZ);
    }

    protected override Quaternion GetSpawnedObjectRotation()
    {
        return transform.rotation;
    }
}
