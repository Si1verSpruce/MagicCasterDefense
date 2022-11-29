using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : Missle
{
    [SerializeField] private float _damage;

    private SphereCollider _damageArea;

    private void Awake()
    {
        _damageArea = GetComponent<SphereCollider>();
        Activate();
    }

    protected override void Deactivate()
    {
        _damageArea.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }

    protected override void OnTargetAchieved()
    {

    }
}
