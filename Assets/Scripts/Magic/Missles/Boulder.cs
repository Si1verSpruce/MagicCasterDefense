using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Boulder : Missle
{
    [SerializeField] private float _damage;

    private BoxCollider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnTargetAchieved()
    {
        Activate();
        _rigidbody.isKinematic = true;
    }

    protected override void Deactivate()
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }
}
