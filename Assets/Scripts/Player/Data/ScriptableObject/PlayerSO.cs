using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player", order = 0)]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerMovementData MovementData { get; private set; }
        [field: SerializeField] public PlayerReusableData ReusableData { get; private set; }
        [field: SerializeField] public PlayerAnimationData AnimationData {get; private set;}
        [field: SerializeField] public StatisticsData StatisticsData {get; private set;}
        [field: SerializeField] public List<Attack> lightAttacks;
    }
