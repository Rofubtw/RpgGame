using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    
    public PlayerTestState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {

    }

    public override void Tick(float deltaTime)
    {
        Vector3 movementDir = new Vector3();
        movementDir.x = stateMachine.InputReader.MovementValue.x;
        movementDir.y = 0;
        movementDir.z = stateMachine.InputReader.MovementValue.y;

        stateMachine.transform.Translate(movementDir * deltaTime);
    }

    public override void Exit()
    {
        
    }
}
