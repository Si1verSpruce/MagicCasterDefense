using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Missle : MonoBehaviour
{
    protected const float Lifetime = 10f;

    [SerializeField] private float _duration;

    private float _currentLifetime;
    private bool _isActive;

    private void Update()
    {
        if (_isActive == false)
            return;

        if (_currentLifetime >= _duration)
        {
            Deactivate();

            if (_currentLifetime >= Lifetime)
                Destroy(gameObject);
        }

        _currentLifetime += Time.deltaTime;
    }

    protected abstract void Deactivate();

    protected virtual void Activate()
    {
        _isActive = true;
    }
}
