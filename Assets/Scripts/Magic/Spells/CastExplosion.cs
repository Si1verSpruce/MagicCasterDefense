using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastExplosion : Spell
{
    public override void Cast(Vector3 position)
    {
        Instantiate(CreatedInstance, position, Quaternion.identity);
    }

    public void Cast(Vector3 position, Instance instance)
    {
        instance.transform.position = position;
        instance.transform.rotation = Quaternion.identity;
        instance.gameObject.SetActive(true);
    }
}
