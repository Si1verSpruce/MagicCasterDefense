using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const float MinPositionX = -3;
    private const float MaxPositionX = 3;
    private const float PositionY = 0;
    private const float PositionZ = 0;

    [SerializeField] private float _interval;
    [SerializeField] private Enemy _unit;
    [SerializeField] private Player _player;

    private float _timeFromSpawn;

    private void Update()
    {
        if (_timeFromSpawn >= _interval)
        {
            Enemy enemy = Instantiate(_unit, new Vector3(Random.Range(MinPositionX, MaxPositionX), PositionY, PositionZ), transform.rotation, transform);
            enemy.Init(_player);

            _timeFromSpawn = 0;
        }
        else
        {
            _timeFromSpawn += Time.deltaTime;
        }
    }
}
