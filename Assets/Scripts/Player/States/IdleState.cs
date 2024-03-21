using UnityEngine;

public class IdleState : PlayerGroundedState
{
    private bool isKatanaDrawn = false; // TODO: Refactor to ReusableData

    public IdleState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        if (stateMachine.PlayerController.Body.velocity.magnitude > 0.2)
        {
            DecelerateHorizontally();
            return;
        }
        ResetVelocity();
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        DrawOrSheathKatana();

    }

    public void DrawOrSheathKatana()
    // TODO: Refactor to use InputReader
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isKatanaDrawn = !isKatanaDrawn;
            stateMachine.PlayerController.Animator.SetBool("IsKatanaDrawn", isKatanaDrawn);
        }
    }

}
