using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private Camera mainCamera;

    private List<Target> targets = new List<Target>();

    public Target currentTarget {get; private set;}


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
           targets.Add(target);
           target.OnDestroyed += RemoveTarget;
        }
    }

    private void RemoveTarget(Target target)
    {
        if (currentTarget == target)
        {
            targetGroup.RemoveMember(target.transform);
            currentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Target>(out Target target))
        {
            RemoveTarget(target);
        }
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) return false;
        Target closestTarget = GetClosestTarget();

        if (closestTarget == null) { return false; }


        currentTarget = closestTarget;
        targetGroup.AddMember(currentTarget.transform, 1f, 2f);

        return true;
    }

    private Target GetClosestTarget()
    {
        Target closestTarget = null;
        float closestTargetDistance = float.PositiveInfinity;

        foreach (Target target in targets)
        {
            Vector2 viewportPoint = mainCamera.WorldToViewportPoint(target.transform.position);

            bool xValueNotBetween1and0 = viewportPoint.x < 0 || viewportPoint.x > 1;
            bool yValueNotBetween1and0 = viewportPoint.y < 0 || viewportPoint.y > 1;

            if (xValueNotBetween1and0 || yValueNotBetween1and0) continue;

            Vector2 toCenter = viewportPoint - new Vector2(0.5f, 0.5f);

            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }

        }

        return closestTarget;
    }

    public void Cancel()
    {
        if (currentTarget == null) return;

        targetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

}
