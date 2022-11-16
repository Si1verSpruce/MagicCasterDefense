using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Fireball : Spell
{
    [SerializeField] private float _damage;
    [SerializeField] private float _duration;

    private SphereCollider _damageArea;
    private float _currentLifetime;

    private void Awake()
    {
        _damageArea = GetComponent<SphereCollider>();
        _currentLifetime = 0;
    }

    private void Update()
    {
        if (_currentLifetime >= _duration)
        {
            _damageArea.enabled = false;

            if (_currentLifetime >= Lifetime)
                Destroy(gameObject);
        }

        _currentLifetime += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.ApplyDamage(_damage);
    }
}
