using System;

public abstract class PlayerBaseState : State
{
    protected string Name;
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(string stateName, PlayerStateMachine stateMachine)
    {
        this.Name = stateName;
        this.stateMachine = stateMachine;
    }
}
