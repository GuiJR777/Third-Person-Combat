using System;
using UnityEngine;

public class FreeLookState : PlayerGroundedState
{
    public FreeLookState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.InputReader.LockOnTarget += OnLockTarget;
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
        stateMachine.PlayerController.InputReader.LockOnTarget -= OnLockTarget;
        stateMachine.PlayerController.InputReader.CancelLockTarget -= OnCancelTarget;
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
        stateMachine.PlayerController.Data.ReusableData.isLockOnTarget = true;
    }

    private void OnCancelTarget()
    {
        // TODO: This method is called automatic if target out of range
        stateMachine.PlayerController.Data.ReusableData.isLockOnTarget = false;
    }
}