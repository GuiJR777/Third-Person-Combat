using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovingState : PlayerBaseState
{

    public PlayerMovingState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
    }

    public override void Enter()
    {
        SetBaseSpeed();
        SetBaseRotationData();
    }

    public override void Tick(float deltaTime)
    {
        GetMovementInput();
    }

    public override void PhysicsTick(float fixedDeltaTime){}

    public override void Exit(){}

    protected void GetMovementInput()
    {
        stateMachine.PlayerController.Data.ReusableData.movementInput = stateMachine.PlayerController.InputReader.MovementValue;
    }

    protected void Move()
    {
        if (stateMachine.PlayerController.Data.ReusableData.movementInput == Vector2.zero)
        {
            return;
        }

        Vector3 movementDirection = GetMovementDirection();
        float targetRotationYAngle = Rotate(movementDirection);
        Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
        float movementSpeed = stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed;
        float slopeSpeedModifier = stateMachine.PlayerController.Data.ReusableData.currentSlopeModifier;
        Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
        Vector3 desiredForce = targetRotationDirection * movementSpeed * slopeSpeedModifier - currentPlayerHorizontalVelocity;

        stateMachine.PlayerController.Body.AddForce(desiredForce, ForceMode.VelocityChange);
    }

    protected Vector3 GetMovementDirection(){
        Vector3 playerInputVector = Vector3.zero;

        playerInputVector.x = stateMachine.PlayerController.Data.ReusableData.movementInput.x;
        playerInputVector.z = stateMachine.PlayerController.Data.ReusableData.movementInput.y;


        return playerInputVector;
    }

    private float Rotate(Vector3 direction)
    {
        float directionAngle = UpdateTargetRotation(direction);

        RotateTowardsTargetRotation();

        return directionAngle;
    }

    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }


        if (directionAngle != stateMachine.PlayerController.Data.ReusableData.currentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }

    private static float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }

    private float AddCameraRotationToAngle(float angle)
    {
        angle += stateMachine.PlayerController.cameraTransform.eulerAngles.y;

        if (angle > 360f)
        {
            angle -= 360f;
        }

        return angle;
    }

    private void UpdateTargetRotationData(float targetAngle)
    {
        stateMachine.PlayerController.Data.ReusableData.currentTargetRotation.y = targetAngle;

        stateMachine.PlayerController.Data.ReusableData.dampedTargetRotationPassedTime.y = 0f;
    }

    protected void RotateTowardsTargetRotation()
    {
        float currentYAngle = stateMachine.PlayerController.Body.rotation.eulerAngles.y;

        if (currentYAngle == stateMachine.PlayerController.Data.ReusableData.currentTargetRotation.y)
        {
            return;
        }

        float smoothedYAngle = Mathf.SmoothDampAngle(
            currentYAngle,
            stateMachine.PlayerController.Data.ReusableData.currentTargetRotation.y,
            ref stateMachine.PlayerController.Data.ReusableData.dampedTargetRotationCurrentVelocity.y,
            stateMachine.PlayerController.Data.ReusableData.timeToReachTargetRotation.y - stateMachine.PlayerController.Data.ReusableData.dampedTargetRotationPassedTime.y
        );

        stateMachine.PlayerController.Data.ReusableData.dampedTargetRotationPassedTime.y += Time.deltaTime;

        Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

        stateMachine.PlayerController.Body.MoveRotation(targetRotation);
    }

    protected Vector3 GetTargetRotationDirection(float targetAngle)
    {
        return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
    }

    protected Vector3 GetPlayerHorizontalVelocity(){
        Vector3 horizontalVelocity = stateMachine.PlayerController.Body.velocity;

        horizontalVelocity.y = 0f;

        return horizontalVelocity;
    }

    protected void SetBaseRotationData()
    {
        stateMachine.PlayerController.Data.ReusableData.timeToReachTargetRotation = stateMachine.PlayerController.Data.MovementData.targetRotationReachTime;
    }

    protected void SetBaseSpeed()
    {
        stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed = stateMachine.PlayerController.Data.MovementData.BaseSpeed;
        stateMachine.PlayerController.Data.ReusableData.movementDecelerationForce = stateMachine.PlayerController.Data.MovementData.DecelerationSpeed;
    }

    protected void ResetVelocity()
    {
        stateMachine.PlayerController.Body.velocity = Vector3.zero;
    }

    protected void DecelerateHorizontally()
    {
        Vector3 horizontalVelocity = GetPlayerHorizontalVelocity();

        Vector3 decelerationForce = -horizontalVelocity * stateMachine.PlayerController.Data.ReusableData.movementDecelerationForce;

        stateMachine.PlayerController.Body.AddForce(decelerationForce, ForceMode.Acceleration);
    }

    protected bool IsMovingHorizontally(float threshold = 0.1f)
    {
        Vector3 playerMovement = GetPlayerHorizontalVelocity();

        Vector2 playerHorizontalMovement = new Vector2(playerMovement.x, playerMovement.z);

        return playerHorizontalMovement.magnitude > threshold;
    }

    protected float GetMovementSpeed()
    {
        float baseMaxSpeed = stateMachine.PlayerController.Data.MovementData.BaseSpeed;
        float currentMaxSpeed = stateMachine.PlayerController.Data.ReusableData.currentMaxSpeed;
        return currentMaxSpeed / baseMaxSpeed * GetMovementDirection().magnitude;
    }

    protected Vector3 GetPlayerVerticalVelocity(){
        return new Vector3(0f, stateMachine.PlayerController.Body.velocity.y, 0f);
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
}
