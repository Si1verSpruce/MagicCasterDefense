using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCaster))]
public class PlayerCastInput : MonoBehaviour
{
    [SerializeField] CastSpellPanel _castPanel;

    private PlayerCaster _cast;
    private Camera _camera;
    private bool _castPanelDowned;

    private void Awake()
    {
        _cast = GetComponent<PlayerCaster>();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        _castPanel.PointerDowned += OnCastPanelPointerDowned;
    }

    private void OnDisable()
    {
        _castPanel.PointerDowned -= OnCastPanelPointerDowned;
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (_castPanelDowned == false)
            return;

        if (context.canceled)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                transform.position = hit.point;
                _cast.OnCast(hit.point);
            }

            _castPanelDowned = false;
        }
    }

    private void OnCastPanelPointerDowned()
    {
        _castPanelDowned = true;
    }
}
