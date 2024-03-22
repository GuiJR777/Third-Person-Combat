using UnityEngine;

public class FreeLookState : PlayerGroundedState
{
    public FreeLookState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        StatesHandler();
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
}