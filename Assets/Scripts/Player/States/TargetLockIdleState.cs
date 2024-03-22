public class TargetLockIdleState : TargetLockState
{
    public TargetLockIdleState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        if (stateMachine.PlayerController.Body.velocity.magnitude > 0.2)
        {
            DecelerateHorizontally();
            return;
        }
        ResetVelocity();
    }

}
