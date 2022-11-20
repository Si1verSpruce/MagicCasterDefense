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
    private bool _castPanelDowned;

    private void Awake()
    {
        _cast = GetComponent<PlayerCaster>();
        _camera = Camera.main;
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (_cast.CurrentSpell == null)
            return;

        if (context.canceled)
        {
            Ray ray = _camera.ScreenPointToRay(Pointer.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.position = hit.point;
                _cast.OnCast(hit.point);
            }

            _castPanelDowned = false;
        }
    }
}
