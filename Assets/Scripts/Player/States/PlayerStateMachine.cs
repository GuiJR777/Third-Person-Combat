using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : StateMachine
{
   public PlayerController PlayerController {get; private set;}

   public PlayerBaseState idleState;

    public PlayerStateMachine(PlayerController playerController)
    {
      PlayerController = playerController;
    }

    public void Awake()
   {
      idleState = new IdleState(this);
   }

   public void Start()
   {
      SwitchState(idleState);
   }
}
