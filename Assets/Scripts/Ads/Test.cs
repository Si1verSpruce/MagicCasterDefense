using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Count());
    }

    private IEnumerator Count()
    {
        bool tr = true;

        while (tr)
        {
            yield return new WaitForSecondsRealtime(1);
        }
    }
}
