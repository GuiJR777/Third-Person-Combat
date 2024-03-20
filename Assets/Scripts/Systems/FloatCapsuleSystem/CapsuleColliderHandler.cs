using System;
using UnityEngine;

[Serializable]
public class CapsuleColliderHandler
{
    public CapsuleColliderData colliderData { get; private set; }
    [field: SerializeField] public DefaultColliderData defaultColliderData { get; private set; }
    [field: SerializeField] public SlopeData slopeData { get; private set; }

    public void Initialize(GameObject gameObject)
    {
        if (colliderData != null)
        {
            return;
        }

        colliderData = new CapsuleColliderData();
        colliderData.Initialize(gameObject);
    }

    public void CalculateCapsuleColliderDimensions()
    {
        SetCapsuleColliderRadius(defaultColliderData.radius);

        float newHeight = defaultColliderData.height * (1f - slopeData.stepHeightPercentage);
        SetCapsuleColliderHeight(newHeight);

        RecalculateCapsuleColliderCenter();

        if (colliderData.collider.height / 2f < colliderData.collider.radius)
        {
            float halfColliderHeight = colliderData.collider.height / 2f;
            SetCapsuleColliderRadius(halfColliderHeight);
        }

        colliderData.UpdateColliderData();
    }

    public void SetCapsuleColliderRadius(float radius)
    {
        colliderData.collider.radius = radius;
    }

    public void SetCapsuleColliderHeight(float height)
    {
        colliderData.collider.height = height;
    }

    public void RecalculateCapsuleColliderCenter()
    {
        float colliderHeightDifference = defaultColliderData.height - colliderData.collider.height;

        Vector3 newColliderCenter = new Vector3(0f, defaultColliderData.centerY + (colliderHeightDifference / 2f), 0f);

        colliderData.collider.center = newColliderCenter;
    }
}
