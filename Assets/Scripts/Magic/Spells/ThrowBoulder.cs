using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoulder : Spell
{
    [SerializeField] private float _yPositionOffset;
    [SerializeField] private float _zPositionOffset;

    public override void Cast(Vector3 targetPosition)
    {
        Vector3 castPosition = new Vector3(targetPosition.x, targetPosition.y + _yPositionOffset, targetPosition.z + _zPositionOffset);

        GameObject missle = Instantiate(SpawnObject, castPosition, Quaternion.identity);
        missle.GetComponent<Boulder>().Init(targetPosition, TimeToTarget);
    }
}
