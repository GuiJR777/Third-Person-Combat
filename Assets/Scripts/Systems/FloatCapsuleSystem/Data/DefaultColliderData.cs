using System;
using UnityEngine;

[Serializable]
public class DefaultColliderData
{
    [field: SerializeField] public float height { get; private set; } = 1.84f;
    [field: SerializeField] public float radius { get; private set; } = 0.25f;
    [field: SerializeField] public float centerY { get; private set; } = 0.92f;
}
