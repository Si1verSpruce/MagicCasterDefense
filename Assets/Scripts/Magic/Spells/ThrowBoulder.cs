using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoulder : Spell
{
    [SerializeField] private float _worldPositionY;
    [SerializeField] private float _worldPositionZ;

    public override void Cast(Vector3 targetPosition)
    {
        Vector3 castPosition = new Vector3(targetPosition.x, _worldPositionY, _worldPositionZ);

        GameObject missle = Instantiate(SpawnObject, castPosition, Quaternion.identity);
        missle.GetComponent<Boulder>().Init(targetPosition, TimeToTarget);
    }
}
