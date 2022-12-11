using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpawner : Spawner, IScaleble
{
    [SerializeField] private Vector3 _startPositionOffset;
    [SerializeField] private Vector3 _minLocalTargetPosition;
    [SerializeField] private Vector3 _maxLocalTargetPosition;
    [SerializeField] private int _missleCount;
    [SerializeField] private float _timeToTarget;

    private int _misslesLeft;
    private Vector3 _lastTargetPosition;

    private void OnEnable()
    {
        _misslesLeft = _missleCount;
    }

    public void Scale(float modifier)
    {
        if (SpawnedObjects[0].Instance.TryGetComponent<IScaleble>(out IScaleble scaleble))
            _missleCount += (int)Mathf.Round(_missleCount / modifier);
    }

    protected override void SpawnPerInterval()
    {
        if (_misslesLeft > 0)
        {
            base.SpawnPerInterval();
        }
    }

    protected override void Spawn()
    {
        _misslesLeft--;
        base.Spawn();
    }

    protected override Instance GetSpawnedInstance()
    {
        return SpawnedObjects[0].Instance;
    }

    protected override Vector3 GetSpawnPosition()
    {
        float positionX = Random.Range(_minLocalTargetPosition.x, _maxLocalTargetPosition.x);
        float positionY = Random.Range(_minLocalTargetPosition.y, _maxLocalTargetPosition.y);
        float positionZ = transform.position.z + Random.Range(_minLocalTargetPosition.z, _maxLocalTargetPosition.z);
        _lastTargetPosition = new Vector3(positionX, positionY, positionZ);

        return _lastTargetPosition + _startPositionOffset;
    }

    protected override Quaternion GetSpawnedObjectRotation()
    {
        return Quaternion.identity;
    }

    protected override void OnInstantiated(Instance instance)
    {
        instance.GetComponent<Missle>().Launch(_lastTargetPosition, _timeToTarget);
        instance.transform.LookAt(_lastTargetPosition);

        if (_misslesLeft == 0)
            StartCoroutine(DisableAfterMissleDisabled(instance));
    }

    private IEnumerator DisableAfterMissleDisabled(Instance missle)
    {
        yield return new WaitUntil(() => missle.gameObject.activeSelf != true);

        gameObject.SetActive(false);
    }
}
