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

    private void Activate()
    {
        IsActive = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        _explosion.gameObject.SetActive(true);
    }

    protected override void Deactivate()
    {
        UpdateState(false);

        foreach (var effect in _fireEffects)
            effect.Stop();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent<Ground>(out Ground ground))
            Activate();
    }

    protected override void OnTargetAchieved()
    {

    }

    protected override void ResetState()
    {
        base.ResetState();
        IsActive = false;
        UpdateState(true);
        _explosion.gameObject.SetActive(false);

        foreach (var effect in _fireEffects)
            effect.Play();
    }

    private void UpdateState(bool isResetted)
    {
        _renderer.enabled = isResetted;
        _collider.enabled = isResetted;
        _rigidbody.isKinematic = !isResetted;
    }
}
