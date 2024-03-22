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
        stateMachine.PlayerController.Data.AnimationData.SetIsKatanaDrawn(true);
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
        if (!stateMachine.PlayerController.Targeter.SelectTarget())
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
            // TODO: Change to targetLockMoveState
            Debug.Log("Target Lock Move");
            return;
        }

        // TODO: Change to targetLockIdleState
        Debug.Log("Target Lock Idle");

    }

    private void OnCancelTarget()
    {
        stateMachine.PlayerController.Targeter.Cancel();
        stateMachine.SwitchState(stateMachine.freeLookState);
    }
}