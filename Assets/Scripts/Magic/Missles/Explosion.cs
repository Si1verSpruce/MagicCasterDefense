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
        IsActive = true;

        if (Duration <= 0.1f)
            Duration = 0.1f;
    }

    public override void Scale(float modifier)
    {
        transform.localScale *= modifier;
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

    protected override void ResetState()
    {
        base.ResetState();
        _damageArea.enabled = true;
    }
}
