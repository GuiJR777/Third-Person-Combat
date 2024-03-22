using System;
using UnityEngine;

public class FreeLookState : PlayerGroundedState
{
    public FreeLookState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.InputReader.LockOnTarget += OnLockTarget;
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        StatesHandler();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputReader.LockOnTarget -= OnLockTarget;
    }

    private void StatesHandler()
    {
        bool isMoving = stateMachine.PlayerController.InputReader.MovementValue != Vector2.zero;

        if (isMoving)
        {
            stateMachine.SwitchState(stateMachine.freeLookMoveState);
            return;
        }

        stateMachine.SwitchState(stateMachine.freeLookIdleState);

    }
    private void OnLockTarget()
    {
        // TODO: Check if is target available
        // TODO: Change to TargetLockState
    }

}