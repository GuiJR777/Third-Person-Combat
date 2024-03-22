using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
   public PlayerController PlayerController {get; private set;}

   public PlayerBaseState freeLookState;
   public PlayerBaseState targetLockState;
   public PlayerBaseState freeLookIdleState;
   public PlayerBaseState freeLookMoveState;
   public PlayerBaseState targetLockIdleState;

    public PlayerStateMachine(PlayerController playerController)
    {
      PlayerController = playerController;
    }

    public void Awake()
   {
      freeLookState = new FreeLookState("FreeLookState", this);
      targetLockState = new TargetLockState("TargetLockState", this);
      freeLookIdleState = new FreeLookIdleState("FreeLookIdle", this);
      freeLookMoveState = new FreeLookMoveState("FreeLookMove", this);
      targetLockIdleState = new TargetLockIdleState("TargetLockIdle", this);
   }

   public void Start()
   {
      SwitchState(freeLookState);
   }
}
