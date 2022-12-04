using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastElectricTrap : Spell
{
    public override void Cast(Vector3 position)
    {
        Instantiate(CreatedInstance, position, Quaternion.identity);
    }
}
