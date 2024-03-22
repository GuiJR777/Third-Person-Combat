using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

    private void OnTriggerEnter(Collider other)
    {
        if (IsATarget(other))
        {
            AddTarget(other.GetComponent<Target>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsATarget(other))
        {
            RemoveTarget(other.GetComponent<Target>());
        }
    }

    private static bool IsATarget(Collider other)
    {
        return other.GetComponent<Target>() != null;
    }

    private void AddTarget(Target target)
    {
        targets.Add(target);
    }

    private void RemoveTarget(Target target)
    {
        targets.Remove(target);
    }
}
