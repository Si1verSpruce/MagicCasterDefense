using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missle : Instance, IScaleble
{
    [SerializeField] protected float BaseDuration;
    [SerializeField] private float _lifetime;

    protected bool IsActive;
    protected float Duration;
    private float _currentLifetime;

    public void Launch(Vector3 targetPosition, float speed)
    {
        StartCoroutine(MoveToTarget(targetPosition, speed));
    }

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        Duration = BaseDuration;
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

    public virtual void Scale(float modifier) { }
    protected virtual void Deactivate() { }
    protected virtual void OnTargetAchieved() { }

    protected virtual void ResetState()
    {
        _currentLifetime = 0;
    }

    protected IEnumerator MoveToTarget(Vector3 targetPosition, float speed)
    {
        float scaledVelocity = speed * Time.deltaTime;

        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, scaledVelocity);

            yield return null;
        }

        OnTargetAchieved();
    }
}
