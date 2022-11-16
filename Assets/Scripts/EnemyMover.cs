using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{
    private Enemy _enemy;
    private float _moveSpeed;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();

        _moveSpeed = _enemy.MoveSpeed;
    }

    private void Update()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * scaledMoveSpeed);
    }
}
