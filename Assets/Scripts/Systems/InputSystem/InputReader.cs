using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActionsMap.IPlayerActions
{

    public Vector2 MovementValue {get; private set;}
    public event Action Jump;
    public event Action Dodge;
    public event Action Sprint;
    public event Action SprintCanceled;
    public event Action LockOnTarget;
    public event Action CancelLockTarget;

    public event Action DrawOrSheat;

    private InputActionsMap _inputAction;

    private void Start()
    {
        _inputAction = new InputActionsMap();
        _inputAction.Player.SetCallbacks(this);

        _inputAction.Player.Enable();
    }

    private void OnDestroy()
    {
        _inputAction.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Jump?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Dodge?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Sprint?.Invoke();
        }

        if (context.canceled)
        {
            SprintCanceled?.Invoke();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }

    public void OnLockOnTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        LockOnTarget?.Invoke();
    }

    public void OnCancelLockTarget(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        CancelLockTarget?.Invoke();
    }

    public void OnDrawOrSheatKatana(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        DrawOrSheat?.Invoke();
    }
}
