using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : Missle
{
    private const float _delayAfterAllTriggers = 1;

    [SerializeField] private int _damage;
    [SerializeField] private int _triggerCount;
    [SerializeField] private ParticleSystem _spark;

    private int _currentTriggerCount;

    public override void Scale(float modifier)
    {
        Duration *= modifier;
    }

    protected override void ResetState()
    {
        base.ResetState();
        IsActive = true;
        _currentTriggerCount = _triggerCount;
        _spark.time = 0;
        _spark.Stop();
        _spark.gameObject.SetActive(false);
    }

    protected override void Deactivate() { }

    private void OnTriggerEnter(Collider collider)
    {
        if (_currentTriggerCount > 0)
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.ApplyDamage(_damage);
                _spark.gameObject.SetActive(true);
                _spark.time = 0;
                _spark.Play();
                _currentTriggerCount--;

                if (_currentTriggerCount <= 0)
                    StartCoroutine(DeactivateAfterDelay());
            }
    }

    private IEnumerator DeactivateAfterDelay()
    {
        float time = 0;

        while (time <= _delayAfterAllTriggers)
        {
            yield return null;

            time += Time.deltaTime;
        }

        gameObject.SetActive(false);
    }
}
