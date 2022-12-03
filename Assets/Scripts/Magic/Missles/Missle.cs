using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Missle : MonoBehaviour, IScaleble
{
    [SerializeField] protected float Duration;
    [SerializeField] protected float Lifetime;

    protected bool IsActive;
    private float _currentLifetime;

    public void Launch(Vector3 targetPosition, float timeToTarget)
    {
        StartCoroutine(MoveToTarget(targetPosition, timeToTarget));
    }

    private void OnEnable()
    {
        ResetState();
    }

    public abstract void Scale(int modifier);

    private void Update()
    {
        if (IsActive == false)
            return;

        if (_currentLifetime >= Duration)
        {
            Deactivate();

            if (_currentLifetime >= Lifetime)
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

    protected IEnumerator MoveToTarget(Vector3 targetPosition, float timeToTarget)
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
