public class TargetLockIdleState : TargetLockState
{
    public TargetLockIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        FaceTarget();

        if (stateMachine.PlayerController.Body.velocity.magnitude > 0.2)
        {
            DecelerateHorizontally();
            return;
        }
        ResetVelocity();
    }

}
