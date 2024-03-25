using UnityEngine;

public class TargetLockMoveState : TargetLockState
{
    private const float SpeedPercentInTargetLock = 0.7f;

    public TargetLockMoveState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed = stateMachine.PlayerController.Data.MovementData.BaseSpeed * SpeedPercentInTargetLock;
        stateMachine.PlayerController.Data.AnimationData.SetIsMoving(true);
        stateMachine.PlayerController.InputReader.Sprint += OnSprint;
        stateMachine.PlayerController.InputReader.SprintCanceled += OnSprintCanceled;
    }


    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        Move();
        FaceTarget();
        stateMachine.PlayerController.Data.AnimationData.SetMovementOnTargetLockSpeed(stateMachine.PlayerController.Data.ReusableData.movementInput);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.Data.AnimationData.SetIsMoving(false);
        stateMachine.PlayerController.InputReader.Sprint -= OnSprint;
        stateMachine.PlayerController.InputReader.SprintCanceled -= OnSprintCanceled;
    }

    private void OnSprint()
    {

    }

    private void OnSprintCanceled()
    {

    }
}