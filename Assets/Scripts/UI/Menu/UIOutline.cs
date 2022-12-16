using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOutline : MonoBehaviour
{
    [SerializeField] private Material _outline;
    [SerializeField] private Image _image;

    public void Enable()
    {
        _image.material = _outline;
    }

    public void Disable()
    {
        _image.material = null;
    }
}
