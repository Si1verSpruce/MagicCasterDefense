using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Boulder : Missle
{
    [SerializeField] private float _damage;
    [SerializeField] private float _glideDistance;
    [SerializeField] private float _glideSpeed;
    [SerializeField] private ParticleSystem _groundHitEffect;

    private BoxCollider _collider;
    private Rigidbody _rigidbody;
    private bool _isGrounded;
    private float _defaultGlideDistance;

    protected override void Init()
    {
        base.Init();
        _collider = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _defaultGlideDistance = _glideDistance;
    }

    public override void Scale(float modifier)
    {
        _glideDistance = _defaultGlideDistance * modifier;
    }

    protected override void ResetState()
    {
        base.ResetState();
        IsActive = false;
        _isGrounded = false;
        _rigidbody.isKinematic = true;
        _groundHitEffect.gameObject.SetActive(false);
    }

    protected override void OnTargetAchieved()
    {
        _collider.enabled = true;
        _rigidbody.isKinematic = true;

        if (_isGrounded == false)
        {
            _groundHitEffect.transform.SetParent(null);
            _groundHitEffect.gameObject.SetActive(true);
            StartCoroutine(MoveToTarget(transform.position + Vector3.forward * _glideDistance, _glideSpeed));
            _isGrounded = true;
        }
        else
        {
            IsActive = true;
        }
    }

    protected override void Deactivate()
    {
        _collider.enabled = false;
        _rigidbody.isKinematic = false;
        _groundHitEffect.transform.position = transform.position;
        _groundHitEffect.transform.SetParent(transform);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }
}
