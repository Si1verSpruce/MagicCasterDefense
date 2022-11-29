using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Missle : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _lifetime;

    private float _currentLifetime;
    private bool _isActive;

    public void Init(Vector3 targetPosition, float velocity)
    {
        StartCoroutine(MoveToTarget(targetPosition, velocity));
    }

    private void Update()
    {
        if (_isActive == false)
            return;

        if (_currentLifetime >= _duration)
        {
            Deactivate();

            if (_currentLifetime >= _lifetime)
                gameObject.SetActive(false);
        }

        _currentLifetime += Time.deltaTime;
    }

    protected abstract void Deactivate();

    protected abstract void OnTargetAchieved();

    protected virtual void Activate()
    {
        _isActive = true;
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition, float timeToTarget)
    {
        float scaledVelocity = Vector3.Distance(transform.position, targetPosition) / timeToTarget * Time.deltaTime;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, scaledVelocity);

            yield return null;
        }

        OnTargetAchieved();
    }
}
