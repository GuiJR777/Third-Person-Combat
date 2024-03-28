using UnityEngine;

public class TargetLockState : PlayerGroundedState
{
    public TargetLockState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.Data.AnimationData.SetIsKatanaDrawn(true);
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
        if (stateMachine.PlayerController.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackState(stateMachine, 0));
            return;
        }

        if (!stateMachine.PlayerController.Targeter.currentTarget)
        {
            OnCancelTarget();
            return;
        }

        if (!stateMachine.PlayerController.Data.AnimationData.IsKatanaDrawn)
        {
            OnCancelTarget();
            return;
        }

        bool isMoving = stateMachine.PlayerController.InputReader.MovementValue != Vector2.zero;

        if (isMoving)
        {
            stateMachine.SwitchState(stateMachine.targetLockMoveState);
            return;
        }

        stateMachine.SwitchState(stateMachine.targetLockIdleState);

    }

    protected void OnCancelTarget()
    {
        stateMachine.PlayerController.Targeter.Cancel();
        stateMachine.SwitchState(stateMachine.freeLookState);
    }
}