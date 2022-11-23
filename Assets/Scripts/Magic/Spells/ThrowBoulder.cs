using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBoulder : Spell
{
    public override void Cast(Vector3 position)
    {
        float positionY = 10;
        float startForce = 3000;
        GameObject missle = Instantiate(SpawnObject, new Vector3(position.x, positionY, position.z), Quaternion.identity);
        missle.GetComponent<Rigidbody>().AddForce(new Vector3(0, -startForce, 0), ForceMode.Impulse);
    }
}
