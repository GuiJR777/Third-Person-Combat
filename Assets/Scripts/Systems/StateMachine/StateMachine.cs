using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    private State _currentState;

    private void Update()
    {
        _currentState?.Tick(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _currentState?.PhysicsTick(Time.fixedDeltaTime);
    }

    public void SwitchState(State newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
