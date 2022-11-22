using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private const float DeactivateRagdollPhysicsDelay = 2;
    private const float FallUndergroundDelay = 4;
    private const float DestroyDelay = 5;

    [SerializeField] private float _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private Rigidbody[] _ragdollRigidbodies;
    [SerializeField] private Rigidbody[] _weapons;

    private Animator _animator;
    private Player _player;
    private Collider _collider;
    private Rigidbody _mainRigidbody;

    public float MoveSpeed => _moveSpeed;
    public int Damage => _damage;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _mainRigidbody = GetComponent<Rigidbody>();

        SetRagdollKinematic(true);
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void SetRagdollKinematic(bool isKinematic)
    {
        foreach (var ragdollRigidbody in _ragdollRigidbodies)
            ragdollRigidbody.isKinematic = isKinematic;

        foreach (var weapon in _weapons)
            weapon.isKinematic = isKinematic;
    }

    private void Die()
    {
        _collider.enabled = false;
        _moveSpeed = 0;
        _animator.enabled = false;
        SetRagdollKinematic(false);

        StartCoroutine(DoAfterDelay(DeactivateRagdollPhysics, DeactivateRagdollPhysicsDelay));
        StartCoroutine(DoAfterDelay(FallUnderground, FallUndergroundDelay));
        StartCoroutine(DoAfterDelay(DestroyThisInstance, DestroyDelay));

        _player.AddMoney(_reward);
    }

    private IEnumerator DoAfterDelay(Action action, float delay)
    {
        float time = 0;

        while (time <= delay)
        {
            yield return null;

            time += Time.deltaTime;
        }

        action();
    }

    private void FallUnderground()
    {
        _mainRigidbody.isKinematic = false;
        _mainRigidbody.useGravity = true;
    }

    private void DestroyThisInstance()
    {
        Destroy(gameObject);
    }

    private void DeactivateRagdollPhysics()
    {
        SetRagdollKinematic(true);
    }
}
