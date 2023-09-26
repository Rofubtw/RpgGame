using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTestState : PlayerBaseState
{
    private float timeRemaining;
    public PlayerTestState(PlayerStateMachine stateMachine, float timerLimit) : base(stateMachine)
    {
        this.timeRemaining = timerLimit;
    }

    public override void Enter()
    {
        Debug.Log("Enter");
        stateMachine.InputReader.JumpEvent += OnJump;
    }

    public override void Tick(float deltaTime)
    {
        
        
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            //Debug.Log(timeRemaining);
        }
        else
        {
            //stateMachine.SwitchState(new PlayerTestState(stateMachine, 5f));
        }
    }

    private void OnJump()
    {
        stateMachine.SwitchState(new PlayerTestState(stateMachine,10f));
    }

    public override void Exit()
    {
        Debug.Log("Exit");
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
