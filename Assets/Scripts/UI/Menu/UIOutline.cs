using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOutline : MonoBehaviour
{
    [SerializeField] private Sprite _outline;

    private Sprite _image;

    private void Awake()
    {
        //_image = Instantiate(, transform);
    }
/*
    public void Enable()
    {
        _image. = _outline;
    }

    public void Disable()
    {
        _image.material = null;
    }*/
}
