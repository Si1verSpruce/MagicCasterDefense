using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firewall : Missle
{
    [SerializeField] private ParticleSystem _fire;

    private float _defaultDuration;

    protected override void Init()
    {
        base.Init();
        _defaultDuration = Duration;
        IsActive = true;
    }

    public override void Scale(float modifier)
    {
        base.Scale(modifier);
        Duration = _defaultDuration * modifier;
    }

    protected override void Deactivate()
    {
        base.Deactivate();
        _fire.Stop();
    }

    protected override void ResetState()
    {
        base.ResetState();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Enemy enemy))
            enemy.ApplyDamage(Damage);
    }
}
