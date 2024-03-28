using UnityEngine;

public class PlayerGroundedState : PlayerMovingState
{
    private SlopeData slopeData;

    public PlayerGroundedState(PlayerStateMachine stateMachine) : base(stateMachine)
    {
        slopeData = stateMachine.PlayerController.capsuleColliderHandler.slopeData;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.PlayerController.InputReader.DrawOrSheat += OnDrawOrSheatKatana;
    }


    public override void PhysicsTick(float fixedDeltaTime)
    {
        base.PhysicsTick(fixedDeltaTime);
        FloatCapsuleCollider();
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.PlayerController.InputReader.DrawOrSheat -= OnDrawOrSheatKatana;
    }

    private void FloatCapsuleCollider()
    {
        Vector3 capsuleColliderCenterInWorldSpace = stateMachine.PlayerController.capsuleColliderHandler.colliderData.collider.bounds.center;

        Ray downwardsRayFromCapsuleCenter = new Ray(capsuleColliderCenterInWorldSpace, Vector3.down);

        if (Physics.Raycast(downwardsRayFromCapsuleCenter, out RaycastHit hit, slopeData.floatRayDistance, stateMachine.PlayerController.layerData.groundLayer, QueryTriggerInteraction.Ignore))
        {
            float groundAngle = Vector3.Angle(hit.normal, -downwardsRayFromCapsuleCenter.direction);

            float slopeSpeedModifier = SetSlopeSpeedModifierAngle(groundAngle);

            if (slopeSpeedModifier == 0)
            {
                return;
            }

            float distanceToFloatingPoint = stateMachine.PlayerController.capsuleColliderHandler.colliderData.colliderCenterInLocalSpace.y * stateMachine.PlayerController.transform.localScale.y - hit.distance;

            if (distanceToFloatingPoint == 0)
            {
                return;
            }

            float amountToLift = distanceToFloatingPoint * slopeData.stepReachForce - GetPlayerVerticalVelocity().y;

            Vector3 liftForce = Vector3.up * amountToLift;

            stateMachine.PlayerController.Body.AddForce(liftForce, ForceMode.VelocityChange);
        }
    }

    private float SetSlopeSpeedModifierAngle(float angle)
    {
        float slopeSpeedModifier = stateMachine.PlayerController.Data.MovementData.slopeSpeedAngles.Evaluate(angle);

        stateMachine.PlayerController.Data.ReusableData.currentSlopeModifier = slopeSpeedModifier;

        return slopeSpeedModifier;
    }

    private void OnDrawOrSheatKatana()
    {
        bool isKatanaDrawn = stateMachine.PlayerController.Data.AnimationData.IsKatanaDrawn;
        stateMachine.PlayerController.Data.AnimationData.SetIsKatanaDrawn(!isKatanaDrawn);
    }


}
