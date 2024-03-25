using System;
using UnityEngine;

[Serializable]
public class PlayerAnimationData
{
    private readonly int IsKatanaDrawnHash = Animator.StringToHash("IsKatanaDrawn");
    private readonly int IsMovingHash = Animator.StringToHash("IsMoving");
    private readonly int IsLockOnTargetHash = Animator.StringToHash("IsLockOnTarget");
    private readonly int SpeedVariableHash = Animator.StringToHash("FreeLookMoveSpeed");
    private readonly int SpeedVariableInXHash = Animator.StringToHash("TargetMoveSpeedX");
    private readonly int SpeedVariableInYHash = Animator.StringToHash("TargetMoveSpeedY");

    private readonly int DrawAnimation = Animator.StringToHash("StandIdle_To_FightIdle");
    private readonly int SheatAnimation = Animator.StringToHash("FightIdle_To_StandIdle");

    private Animator _animator;
    private bool _isKatanaDrawn = false;
    private bool _isMoving = false;
    private bool _isLockOnTarget = false;


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
        if (_isKatanaDrawn == value) return;

        _isKatanaDrawn = value;
        _animator.SetBool(IsKatanaDrawnHash, value);

        if (value)
        {
            _animator.Play(DrawAnimation);
        }
        else
        {
            _animator.Play(SheatAnimation);
        }
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

    public void SetMovementOnTargetLockSpeed(Vector2 inputVector)
    {
        _animator.SetFloat(SpeedVariableInXHash, inputVector.x);
        _animator.SetFloat(SpeedVariableInYHash, inputVector.y);
    }
}

