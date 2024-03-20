using System;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [field: SerializeField][field: Range(2f, 12f)] public float BaseSpeed;
    [field: SerializeField][field: Range(2f, 12f)] public float DecelerationSpeed;
    [field: SerializeField] public Vector3 targetRotationReachTime;
    [field: SerializeField] public AnimationCurve slopeSpeedAngles { get; private set; }
}
