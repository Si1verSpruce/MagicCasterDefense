using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] GameObject _firstObject;
    [SerializeField] GameObject _secondObject;

    public void SwitchObjects()
    {
        if (_firstObject.activeSelf)
        {
            _firstObject.SetActive(false);
            _secondObject.SetActive(true);
        }
        else
        {
            _firstObject.SetActive(true);
            _secondObject.SetActive(false);
        }
    }
}
