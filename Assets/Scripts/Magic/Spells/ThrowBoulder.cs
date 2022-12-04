using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoulder : Spell
{
    [SerializeField] private float _worldPositionY;
    [SerializeField] private float _worldPositionZ;
    [SerializeField] private float _positionZOffset;

    public override void Cast(Vector3 targetPosition)
    {
        Vector3 castPosition = new Vector3(targetPosition.x, _worldPositionY, _worldPositionZ);

        Instance missle = Instantiate(CreatedInstance, castPosition, Quaternion.identity);
        ((Boulder)missle).Launch(targetPosition, TimeToTarget);
    }
}
