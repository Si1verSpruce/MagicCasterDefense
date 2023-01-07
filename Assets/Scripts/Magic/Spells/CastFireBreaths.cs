using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CastFireBreaths : Spell
{
    public override void Cast(Instance createdInstance, Vector3 targetPosition)
    {
        ResetInstance(createdInstance, Vector3.zero, Quaternion.identity);
    }
}
