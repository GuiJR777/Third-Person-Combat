using UnityEngine;

public class FreeLookMoveState : PlayerGroundedState
{
    private readonly int SpeedVariableHash = Animator.StringToHash("FreeLookMoveSpeed");
    public FreeLookMoveState(string stateName, PlayerStateMachine stateMachine) : base(stateName, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.InputReader.Sprint += OnSprint;
        stateMachine.PlayerController.InputReader.SprintCanceled += OnSprintCanceled;
    }


    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);

        Move();
        stateMachine.PlayerController.Animator.SetFloat(SpeedVariableHash, GetMovementSpeed());
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputReader.Sprint -= OnSprint;
        stateMachine.PlayerController.InputReader.SprintCanceled -= OnSprintCanceled;
    }

    private void OnSprint()
    {
        float baseSpeed = stateMachine.PlayerController.Data.MovementData.BaseSpeed;
        stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed = baseSpeed * 2;
    }

    private void OnSprintCanceled()
    {
        float baseSpeed = stateMachine.PlayerController.Data.MovementData.BaseSpeed;

        if (stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed > baseSpeed)
        {
            stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed = baseSpeed;
        }

    }

}
