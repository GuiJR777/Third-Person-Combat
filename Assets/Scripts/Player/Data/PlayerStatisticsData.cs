using System;
using UnityEngine;

[Serializable]
public class StatisticsData
{
    [field: SerializeField][field: Range(1f, 100f)] public float Damage;
}