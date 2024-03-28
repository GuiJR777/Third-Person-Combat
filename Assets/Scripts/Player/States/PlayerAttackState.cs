using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttackState : PlayerMovingState
{
    private Attack attack;
    private float previousFrameTime;
    private bool isForceApplied = false;

    public PlayerAttackState(PlayerStateMachine stateMachine, int attackIndex) : base(stateMachine)
    {
        attack = stateMachine.PlayerController.Data.lightAttacks[attackIndex];
    }

    public override void Enter()
    {
        if (!stateMachine.PlayerController.Data.AnimationData.IsKatanaDrawn)
        {
            stateMachine.PlayerController.Data.AnimationData.SetIsKatanaDrawn(true);
        }

        stateMachine.PlayerController.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
    }

    public override void Exit()
    {
    }

    public override void PhysicsTick(float fixedDeltaTime)
    {
        FaceTarget();
        if (stateMachine.PlayerController.Body.velocity.magnitude > 0.2)
        {
            DecelerateHorizontally();
            return;
        }
        ResetVelocity();
    }

    public override void Tick(float deltaTime)
    {
        float normalizedTime = GetNormalizedTime();

        if (normalizedTime >= attack.ForceTime && !isForceApplied)
        {
            TryApplyForce();
        }

        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            if (stateMachine.PlayerController.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        else
        {
            if (stateMachine.PlayerController.Targeter.currentTarget != null)
            {
                stateMachine.PlayerController.Animator.CrossFadeInFixedTime("TargetLockIdle", 0.2f);
                stateMachine.SwitchState(stateMachine.targetLockState);
                return;
            }

            stateMachine.PlayerController.Animator.CrossFadeInFixedTime("FightIdle", 0.2f);
            stateMachine.SwitchState(stateMachine.freeLookState);
            return;
        }

        previousFrameTime = normalizedTime;
    }

    private float GetNormalizedTime()
    {
        const int baseLayerIndex = 0;
        Animator animator = stateMachine.PlayerController.Animator;
        AnimatorStateInfo currentInfo = animator.GetCurrentAnimatorStateInfo(baseLayerIndex);
        AnimatorStateInfo nextInfo = animator.GetNextAnimatorStateInfo(baseLayerIndex);

        if (animator.IsInTransition(baseLayerIndex) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        else if (!animator.IsInTransition(baseLayerIndex) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }

        return 0;
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) return;

        if (normalizedTime < attack.ComboAttackTime) return;

        stateMachine.SwitchState(new PlayerAttackState(stateMachine, attack.ComboStateIndex));
    }

    private void TryApplyForce()
    {
        stateMachine.PlayerController.Body.AddForce(stateMachine.PlayerController.transform.forward * attack.Force, ForceMode.Impulse);
        isForceApplied = true;
    }

}
