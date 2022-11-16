using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody[] _ragdollRigidbodies;

    private float _stageModifier;
    private Animator _animator;

    public float MoveSpeed => _moveSpeed;

    private void Awake()
    {
        _stageModifier = 1;
        _moveSpeed *= _stageModifier;
        _animator = GetComponent<Animator>();

        for (int i = 0; i < _ragdollRigidbodies.Length; i++)
            _ragdollRigidbodies[i].isKinematic = true;
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            ActivateRagdoll();
    }

    private void ActivateRagdoll()
    {
        _animator.enabled = false;
        _moveSpeed = 0;

        for (int i = 0; i < _ragdollRigidbodies.Length; i++)
            _ragdollRigidbodies[i].isKinematic = false;
    }
}
