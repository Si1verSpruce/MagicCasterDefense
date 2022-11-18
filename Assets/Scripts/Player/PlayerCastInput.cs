using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCaster))]
public class PlayerCastInput : MonoBehaviour
{
    private PlayerCaster _cast;
    private Camera _camera;

    private void Awake()
    {
        _cast = GetComponent<PlayerCaster>();
        _camera = Camera.main;
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.position = hit.point;
                _cast.OnCast(hit.point);
            }
        }
    }
}
