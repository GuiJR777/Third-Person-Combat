using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    private readonly int IsKatanaDrawnHash = Animator.StringToHash("IsKatanaDrawn");
    private readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private readonly int IsLockOnTargetHash = Animator.StringToHash("IsLockOnTarget");
    private readonly int SpeedVariableHash = Animator.StringToHash("FreeLookMoveSpeed");

    private Animator _animator;
    private bool _isKatanaDrawn;
    private bool _isMoving;
    private bool _isLockOnTarget;


    public ref bool IsKatanaDrawn
    {
        get
        {
            return ref _isKatanaDrawn;
        }
    }

    public ref bool IsMoving
    {
        get
        {
            return ref _isMoving;
        }
    }

    public ref bool IsLockOnTarget
    {
        get
        {
            return ref _isLockOnTarget;
        }
    }

    public void SetAnimator(Animator animator)
    {
        _animator = animator;
    }

    public void SetIsKatanaDrawn(bool value)
    {
        _isKatanaDrawn = value;
        _animator.SetBool(IsKatanaDrawnHash, value);
    }

    public void SetIsMoving(bool value)
    {
        _isMoving = value;
        _animator.SetBool(IsMovingHash, value);
    }

    public void SetIsLockOnTarget(bool value)
    {
        _isLockOnTarget = value;
        _animator.SetBool(IsLockOnTargetHash, value);
    }

    public void SetMovementSpeed(float speed)
    {
        _animator.SetFloat(SpeedVariableHash, speed);
    }
}

