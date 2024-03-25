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
            stateMachine.SwitchState(stateMachine.targetLockMoveState);
            return;
        }

        stateMachine.SwitchState(stateMachine.targetLockIdleState);

    }

    protected void FaceTarget()
    {
        Target target = stateMachine.PlayerController.Targeter.currentTarget;

        if (target == null) return;

        Vector3 targetDirection = target.transform.position - stateMachine.PlayerController.transform.position;
        targetDirection.y = 0;

        stateMachine.PlayerController.transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        UpdateTargetRotation(targetDirection, false);

    }

    private void OnCancelTarget()
    {
        stateMachine.PlayerController.Targeter.Cancel();
        stateMachine.SwitchState(stateMachine.freeLookState);
    }
}