using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastMeteorRain : Spell
{
    public override void Cast(Vector3 position)
    {
        Instantiate(SpawnObject, position, Quaternion.identity);
    }
}