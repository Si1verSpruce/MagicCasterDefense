using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Missle : MonoBehaviour
{
    [SerializeField] protected float Duration;
    [SerializeField] private float _lifetime;

    protected bool IsActive;
    private float _currentLifetime;

    public void Init(Vector3 targetPosition, float velocity)
    {
        StartCoroutine(MoveToTarget(targetPosition, velocity));
    }

    private void OnEnable()
    {
        ResetState();
    }

    private void Update()
    {
        if (IsActive == false)
            return;

        if (_currentLifetime >= Duration)
        {
            Deactivate();

            if (_currentLifetime >= _lifetime)
                gameObject.SetActive(false);
        }

        _currentLifetime += Time.deltaTime;
    }

    protected abstract void Deactivate();

    protected virtual void OnTargetAchieved() { }

    protected virtual void ResetState()
    {
        _currentLifetime = 0;
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
