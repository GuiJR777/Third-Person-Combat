using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
   public PlayerController PlayerController {get; private set;}

   public PlayerBaseState idleState;
   public PlayerBaseState freeLookMoveState;

    public PlayerStateMachine(PlayerController playerController)
    {
      PlayerController = playerController;
    }

    public void Awake()
   {
      idleState = new IdleState("Idle", this);
      freeLookMoveState = new FreeLookMoveState("FreeLookMove", this);
   }

   public void Start()
   {
      SwitchState(idleState);
   }
}
