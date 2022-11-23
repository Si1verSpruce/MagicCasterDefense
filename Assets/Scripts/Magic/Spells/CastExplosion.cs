using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastExplosion : Spell
{
    public override void Cast(Vector3 position)
    {
        Instantiate(Missle, position, Quaternion.identity);
    }
}
