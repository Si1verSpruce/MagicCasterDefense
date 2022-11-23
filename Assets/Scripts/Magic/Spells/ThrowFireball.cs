using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ThrowFireball : Spell
{
    [SerializeField] private ParticleSystem _explosion;
    private SphereCollider _damageArea;

    private void Awake()
    {
        _damageArea = GetComponent<SphereCollider>();
    }

    protected override void Activate()
    {
        base.Activate();
        _damageArea.enabled = true;
        _explosion.Play();
    }

    protected override void Deactivate()
    {
        _damageArea.enabled = false;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Ground>(out Ground ground))
            Activate();
    }
}
