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
    private Vector2 _displaySize;

    private void Awake()
    {
        _caster = GetComponent<PlayerCaster>();
        _camera = Camera.main;
        int _displayWidth = _camera.pixelWidth;
        int _displayHeight = _camera.pixelHeight;
        _displaySize = new Vector2(_displayWidth, _displayHeight);
    }

    private void Update()
    {
        _isOverUI = EventSystem.current.IsPointerOverGameObject();
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (_isOverUI)
            return;

        if (context.canceled)
        {
            var pointerPosition = Pointer.current.position.ReadValue();

            if (pointerPosition.x >= 0 && pointerPosition.x < _displaySize.x &&
                pointerPosition.y >= 0 && pointerPosition.y < _displaySize.y)
            {
                Ray ray = _camera.ScreenPointToRay(pointerPosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);

                foreach (var hit in hits)
                    if (hit.transform.TryGetComponent<Ground>(out Ground ground))
                    {
                        _caster.OnCastInput(hit.point);

                        return;
                    }
            }
        }
    }
}
