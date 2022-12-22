using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Screen : MonoBehaviour
{
    [SerializeField] protected Button RestartSceneButton;
    [SerializeField] protected SessionRestarter _restarter;

    protected void RestartSession()
    {
        _restarter.Restart();
        gameObject.SetActive(false);
    }
}
