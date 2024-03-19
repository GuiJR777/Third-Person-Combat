
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerMovingState
{
    public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        GetMovementInput();
        StatesHandler();
    }

    private void StatesHandler()
    {
        if (stateMachine.PlayerController.InputReader.MovementValue == Vector2.zero)
        {
            stateMachine.SwitchState(stateMachine.idleState);
            return;
        }


    }

}
