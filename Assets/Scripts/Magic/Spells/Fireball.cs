using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Fireball : Spell
{
    private SphereCollider _damageArea;

    private void Awake()
    {
        _damageArea = GetComponent<SphereCollider>();
    }

    protected override void Deactivate()
    {
        _damageArea.enabled = false;
    }
}
