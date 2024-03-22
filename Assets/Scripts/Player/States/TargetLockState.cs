using UnityEngine;

public class TargetLockState : PlayerGroundedState
{
    public TargetLockState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.Data.AnimationData.SetIsLockOnTarget(true);
        stateMachine.PlayerController.InputReader.CancelLockTarget += OnCancelTarget;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        StatesHandler();
    }

    public override void Exit()
    {
        base.Exit();
         stateMachine.PlayerController.Data.AnimationData.SetIsLockOnTarget(false);
         stateMachine.PlayerController.InputReader.CancelLockTarget -= OnCancelTarget;
    }

    private void StatesHandler()
    {
        bool isMoving = stateMachine.PlayerController.InputReader.MovementValue != Vector2.zero;

        if (isMoving)
        {
            // TODO: Change to targetLockMoveState
            return;
        }

        stateMachine.SwitchState(stateMachine.targetLockIdleState);

    }

    private void OnCancelTarget()
    {
        // TODO: This method is called automatic if target out of range
        // TODO: Change to LookFreeState
    }
}