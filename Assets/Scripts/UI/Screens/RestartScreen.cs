using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RestartScreen : MonoBehaviour
{
    [SerializeField] protected SessionRestarter Restarter;

    protected abstract void Deactivate();

    public void RestartSession()
    {
        Restarter.Restart();
        Deactivate();
    }
}
