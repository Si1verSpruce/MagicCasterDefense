using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : MonoBehaviour
{
    [SerializeField] private float _interval;
    [SerializeField] private GameObject _spawnObject;

    private float _timeFromSpawn;
    protected Vector3 SpawnPosition;
    protected GameObject LastSpawnedObject;

    private void Update()
    {
        if (_timeFromSpawn >= _interval)
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
        LastSpawnedObject = Instantiate(_spawnObject, new Vector3(SpawnPosition.x, SpawnPosition.y, SpawnPosition.z), transform.rotation, transform);
    }
}
