using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cursor : MonoBehaviour
{
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        transform.position = new Vector3(mousePosition.x, 0.5f, mousePosition.y);
    }
}
