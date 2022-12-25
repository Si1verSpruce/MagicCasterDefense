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
    private bool _isCastReady;
    private Vector2 _displaySize;

    private void Awake()
    {
        _caster = GetComponent<PlayerCaster>();
        _camera = Camera.main;
        int _displayWidth = _camera.pixelWidth;
        int _displayHeight = _camera.pixelHeight;
        _displaySize = new Vector2(_displayWidth, _displayHeight);
    }

    public void OnCast(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _isCastReady = !CheckIsOverUI();
        }

        if (context.canceled && _isCastReady)
        {
            var pointerPosition = Pointer.current.position.ReadValue();

            if (CheckIsInsideDisplay(pointerPosition) && CheckIsOverUI() == false)
            {
                Ray ray = _camera.ScreenPointToRay(pointerPosition);
                RaycastHit[] hits = Physics.RaycastAll(ray);

                foreach (var hit in hits)
                {
                    if (hit.transform.TryGetComponent(out Ground ground))
                    {
                        _caster.OnCastInput(hit.point);

                        return;
                    }
                }
            }
        }
    }

    private bool CheckIsOverUI()
    {
        PointerEventData data = new PointerEventData(EventSystem.current);
        List<RaycastResult> hits = new List<RaycastResult>();
        var pointerPosition = Pointer.current.position.ReadValue();
        data.position = pointerPosition;
        EventSystem.current.RaycastAll(data, hits);

        if (hits.Count > 0)
            return true;

        return false;
    }

    private bool CheckIsInsideDisplay(Vector2 position)
    {
        return position.x >= 0 && position.x < _displaySize.x &&
               position.y >= 0 && position.y < _displaySize.y;
    }
}
