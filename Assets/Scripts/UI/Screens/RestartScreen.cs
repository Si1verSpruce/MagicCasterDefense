using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class RestartScreen : MonoBehaviour
{
    [SerializeField] protected Button RestartSceneButton;
    [SerializeField] protected SessionRestarter _restarter;

    protected abstract void Deactivate();

    protected void RestartSession()
    {
        _restarter.Restart();
        Deactivate();
    }
}
