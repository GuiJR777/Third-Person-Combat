using UnityEngine;

public class FreeLookMoveState : PlayerGroundedState
{
    public FreeLookMoveState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        Move();
        stateMachine.PlayerController.Animator.SetFloat("FreeLookMoveSpeed", GetMovementSpeed());
    }

    public override void Tick(float deltaTime)
    {
        base.Tick(deltaTime);
        SprintHandler();
    }

    private void SprintHandler()
    {
        // TODO: Refactor to use InputReader
        float baseSpeed = stateMachine.PlayerController.Data.MovementData.FreeLookSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed = baseSpeed * 2;
            return;
        }

        stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed = baseSpeed;
    }
}
