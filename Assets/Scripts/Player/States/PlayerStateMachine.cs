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
      freeLookState = new FreeLookState(this);
      targetLockState = new TargetLockState(this);
      freeLookIdleState = new FreeLookIdleState(this);
      freeLookMoveState = new FreeLookMoveState(this);
      targetLockIdleState = new TargetLockIdleState(this);
   }

   public void Start()
   {
      SwitchState(freeLookState);
   }
}
