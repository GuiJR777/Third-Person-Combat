using System;
using UnityEngine;

[Serializable]
public class PlayerReusableData
{
    public Vector2 movementInput { get; set; }
    public float movementDecelerationForce { get; set; } = 1f;
    public bool shouldWalk { get; set; }

    private Vector3 _currentTargetRotation;
    private Vector3 _timeToReachTargetRotation;
    private Vector3 _dampedTargetRotationCurrentVelocity;
    private Vector3 _dampedTargetRotationPassedTime;
    private float _currentMaxSpeed;

    public ref Vector3 currentTargetRotation
    {
        get
        {
            return ref _currentTargetRotation;
        }
    }

    public ref Vector3 timeToReachTargetRotation
    {
        get
        {
            return ref _timeToReachTargetRotation;
        }
    }

    public ref Vector3 dampedTargetRotationCurrentVelocity
    {
        get
        {
            return ref _dampedTargetRotationCurrentVelocity;
        }
    }

    public ref Vector3 dampedTargetRotationPassedTime
    {
        get
        {
            return ref _dampedTargetRotationPassedTime;
        }
    }

    public ref float currentMaxSpeed
    {
        get
        {
            return ref _currentMaxSpeed;
        }
    }

    // public PlayerRotationData rotationData { get; set; }

}
