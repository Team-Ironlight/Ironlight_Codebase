using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : BaseState
{
    PlayerStateManager _stateManager;
    MovementComponent _movement;

    public MoveState(PlayerStateManager state) : base(state.gameObject)
    {
        _stateManager = state;
    }
    public override void OnEnter()
    {
        Debug.Log("Entering Move State");
        _movement = _stateManager.movement;
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Move State");
    }

    public override Type Tick()
    {
        Debug.Log("Currently in Move State");

        if(_stateManager.vertical == 0 && _stateManager.horizontal == 0)
        {
            return typeof(IdleState);
        }

        else
        {
            _movement.CalculateMoveDir(_stateManager.vertical, _stateManager.horizontal);
            _movement.CalculateMoveAmount(_stateManager.vertical, _stateManager.horizontal);

            Debug.Log(_movement.moveDir);
        }

        



        return null;
    }
}
