using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : Instance
{
    [SerializeField] private float _health;
    [SerializeField] private EnemyParameters _parameters;
    [SerializeField] private Rigidbody[] _ragdollRigidbodies;
    [SerializeField] private Rigidbody[] _weapons;

    private Animator _animator;
    private Player _player;
    private Collider _collider;
    private Rigidbody _mainRigidbody;
    private float _currentMoveSpeed;
    private Vector3[] _weaponStartPositions;
    private Quaternion[] _weaponStartRotations;

    public float MoveSpeed => _currentMoveSpeed;
    public int Damage => _parameters.Damage;

    public void Init(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        _mainRigidbody = GetComponent<Rigidbody>();

        int weaponCount = _weapons.Length;
        _weaponStartPositions = new Vector3[weaponCount];
        _weaponStartRotations = new Quaternion[weaponCount];

        for (int i = 0; i < _weapons.Length; i++)
        {
            _weaponStartPositions[i] = _weapons[i].transform.localPosition;
            _weaponStartRotations[i] = _weapons[i].transform.localRotation;
        }
    }

    private void OnEnable()
    {
        UpdateState(true);
        _mainRigidbody.isKinematic = true;

        for (int i = 0; i < _weapons.Length; i++)
        {
            _weapons[i].transform.localPosition = _weaponStartPositions[i];
            _weapons[i].transform.localRotation = _weaponStartRotations[i];
        }
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
        UpdateState(false);

        StartCoroutine(DoAfterDelay(FallUnderground, _parameters.FallUndergroundDelay));
        StartCoroutine(DoAfterDelay(DisableThis, _parameters.DeactivateDelay));

        _player.AddMoney(_parameters.Reward);
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
        SetRagdollKinematic(true);
        _mainRigidbody.isKinematic = false;
    }

    private void DisableThis()
    {
        gameObject.SetActive(false);
    }

    private void UpdateState(bool isActive)
    {
        _collider.enabled = isActive;
        _currentMoveSpeed = _parameters.MoveSpeed * Convert.ToInt32(isActive);
        _animator.enabled = isActive;
        SetRagdollKinematic(isActive);
    }
}
