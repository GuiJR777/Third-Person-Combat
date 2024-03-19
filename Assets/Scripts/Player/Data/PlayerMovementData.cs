using System;
using UnityEngine;

[Serializable]
public class PlayerMovementData
{
    [field: SerializeField][field: Range(2f, 12f)] public float FreeLookSpeed;
    [field: SerializeField] public Vector3 targetRotationReachTime;
}
