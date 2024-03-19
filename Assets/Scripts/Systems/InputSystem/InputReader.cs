using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActionsMap.IPlayerActions
{

    public Vector2 MovementValue {get; private set;}
    public event Action Jump;
    public event Action Dodge;
    public event Action Sprint;

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
        Debug.Log("Jump");
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Dodge?.Invoke();
        Debug.Log("Dodge");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Sprint?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
    }
}
