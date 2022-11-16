using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _moveSpeed;

    private float _stageModifier;

    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _stageModifier = 1;

        _moveSpeed *= _stageModifier;
    }
}
