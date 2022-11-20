using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class BoulderThrow : Spell
{
    [SerializeField] private float _startForce;

    private BoxCollider _damageArea;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _damageArea = GetComponent<BoxCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        transform.position = new Vector3(transform.position.x, 5, transform.position.z);
        _rigidbody.AddForce(Vector3.down * _startForce, ForceMode.Impulse);
    }

    protected override void Deactivate()
    {
        _damageArea.enabled = false;
    }
}
