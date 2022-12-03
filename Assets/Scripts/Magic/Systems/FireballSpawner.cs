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

    private void Start()
    {
        _misslesLeft = _missleCount;
    }

    public void Scale(int modifier)
    {
        if (SpawnedObject.TryGetComponent<IScaleble>(out IScaleble scaleble))
            scaleble.Scale(modifier);
    }

    protected override void Spawn()
    {
        if (_misslesLeft > 0)
        {
            _misslesLeft--;
            base.Spawn();
        }
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

    protected override void OnObjectSpawned(GameObject instance)
    {
        instance.GetComponent<Missle>().Launch(_lastTargetPosition, _timeToTarget);
        instance.transform.LookAt(_lastTargetPosition);

        if (_misslesLeft == 0)
            StartCoroutine(DisableAfterMissleDisabled(instance));
    }

    private IEnumerator DisableAfterMissleDisabled(GameObject missle)
    {
        yield return new WaitUntil(() => missle.gameObject.activeSelf != true);

        gameObject.SetActive(false);
    }
}
