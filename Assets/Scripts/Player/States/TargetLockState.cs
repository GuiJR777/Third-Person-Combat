using UnityEngine;

public class TargetLockState : PlayerGroundedState
{
    public TargetLockState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
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
            Debug.Log("Moving With Katana");
            return;
        }

        stateMachine.SwitchState(stateMachine.targetLockIdleState);

    }
}