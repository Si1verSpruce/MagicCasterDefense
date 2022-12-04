using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastExplosion : Spell
{
    public override void Cast(Instance createdInstance, Vector3 targetPosition)
    {
        ResetInstance(createdInstance, targetPosition, Quaternion.identity);
    }
}
