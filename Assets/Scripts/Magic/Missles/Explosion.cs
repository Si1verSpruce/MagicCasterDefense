using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Explosion : Missle
{
    [SerializeField] private float _damage;

    private SphereCollider _damageArea;
    private Vector3 _baseSizeScale;

    public void Init()
    {
        _damageArea = GetComponent<SphereCollider>();
        IsActive = true;

        if (Duration <= 0.1f)
            Duration = 0.1f;
    }

    public override void Scale(float modifier)
    {
        ResetParameters();
        transform.localScale *= modifier;
    }

    protected override void Deactivate()
    {
        _damageArea.enabled = false;
    }

    private void ResetParameters()
    {
        transform.localScale = _baseSizeScale;
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
