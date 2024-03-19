using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, InputActionsMap.IPlayerActions
{

    public Vector2 MovementValue {get; private set;}
    public event Action Jump;
    public event Action Dodge;

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
}
