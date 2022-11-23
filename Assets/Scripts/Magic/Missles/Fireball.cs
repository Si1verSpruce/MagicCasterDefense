using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Fireball : Missle
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private ParticleSystem[] _fireEffects;

    private MeshRenderer _renderer;
    private SphereCollider _collider;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    protected override void Activate()
    {
        base.Activate();
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        _explosion.gameObject.SetActive(true);
    }

    protected override void Deactivate()
    {
        _renderer.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;

        foreach (var effect in _fireEffects)
            effect.Stop();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Ground>(out Ground ground))
            Activate();
    }
}
