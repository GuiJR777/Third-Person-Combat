public class FreeLookIdleState : FreeLookState
{
    public FreeLookIdleState(PlayerStateMachine stateMachine) : base(stateMachine)
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
