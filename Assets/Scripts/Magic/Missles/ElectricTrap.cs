using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTrap : Missle
{
    private const float _delayAfterAllTriggers = 1;

    [SerializeField] private int _damage;
    [SerializeField] private float _positionY;
    [SerializeField] private int _triggerCount;
    [SerializeField] private ParticleSystem _spark;

    private void Awake()
    {
        transform.position = new Vector3(transform.position.x, _positionY, transform.position.z);
    }

    protected override void Deactivate() { }

    private void OnTriggerEnter(Collider collider)
    {
        if (_triggerCount > 0)
            if (collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.ApplyDamage(_damage);
                _spark.gameObject.SetActive(true);
                _spark.time = 0;
                _spark.Play();
                _triggerCount--;

                if (_triggerCount <= 0)
                    StartCoroutine(DestroyAfterDelay());
            }
    }

    private IEnumerator DestroyAfterDelay()
    {
        float time = 0;

        while (time <= _delayAfterAllTriggers)
        {
            yield return null;

            time += Time.deltaTime;
        }

        Destroy(gameObject);
    }
}
