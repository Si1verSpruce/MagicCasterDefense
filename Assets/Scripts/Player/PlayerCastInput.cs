using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCaster))]
public class PlayerCastInput : MonoBehaviour
{
    private PlayerCaster _caster;
    private Camera _camera;
    private bool _isOverUI;

    private void Awake()
    {
        _caster = GetComponent<PlayerCaster>();
        _camera = Camera.main;
    }

    private void Update()
    {
        _isOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (_caster.CurrentSpell == null)
            return;

        if (_isOverUI)
            return;

        if (context.canceled)
        {
            Ray ray = _camera.ScreenPointToRay(Pointer.current.position.ReadValue());
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
                if (hit.transform.TryGetComponent<Ground>(out Ground ground))
                {
                    transform.position = hit.point;
                    _caster.OnCastInput(hit.point);

                    return;
                }
        }
    }
}
