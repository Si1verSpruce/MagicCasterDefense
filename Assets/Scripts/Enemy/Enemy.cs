using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private int _reward;
    [SerializeField] private Rigidbody[] _ragdollRigidbodies;
    [SerializeField] private Rigidbody[] _weapons;

    private Animator _animator;
    private Player _player;
    private Collider _collider;

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

        SetKinematic(true);
    }

    public void ApplyDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Die();
    }

    private void SetKinematic(bool isKinematic)
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

        SetKinematic(false);

        _player.AddMoney(_reward);
    }
}
