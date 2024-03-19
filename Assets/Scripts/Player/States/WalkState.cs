using UnityEngine;

public class WalkState : PlayerGroundedState
{
    public WalkState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        Move();
    }
}
