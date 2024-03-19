using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine
{
    private State _currentState;

    public void Tick(float delta)
    {
        _currentState?.Tick(delta);
    }

    public void PhysicsTick(float fixedDelta)
    {
        _currentState?.PhysicsTick(fixedDelta);
    }

    public void SwitchState(State newState)
    {
        if ( _currentState == newState ){ return; }

        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }
}
