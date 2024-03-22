using System;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup targetGroup;

    private List<Target> targets = new List<Target>();

    public Target currentTarget {get; private set;}

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

        currentTarget = targets[0];
        targetGroup.AddMember(currentTarget.transform, 1f, 2f);

        return true;
    }

    public void Cancel()
    {
        if (currentTarget == null) return;

        targetGroup.RemoveMember(currentTarget.transform);
        currentTarget = null;
    }

}
