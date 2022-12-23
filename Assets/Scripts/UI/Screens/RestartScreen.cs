using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
