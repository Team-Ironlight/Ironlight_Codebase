using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_MoveState : PLY_BaseState
{
    PLY_StateManager _stateManager;
    PLY_MovementComponent _movement;

    public PLY_MoveState(PLY_StateManager state) : base(state.gameObject)
    {
        _stateManager = state;
        _movement = _stateManager.movement;
    }
    public override void OnEnter()
    {
        Debug.Log("MoveState: OnEnter");
        _movement.vMoveInput = Vector2.zero;
    }

    public override void OnExit()
    {
        Debug.Log("MoveState: OnExit");
        _movement.vMoveInput = Vector2.zero;
    }

    public override Type Tick()
    {
        //Debug.Log("Currently in Move State");

        if(_stateManager.vertical == 0 && _stateManager.horizontal == 0)
        {
            return typeof(PLY_IdleState);
        }

        _movement.vMoveInput = new Vector2(_stateManager.horizontal, _stateManager.vertical);
        return null;
    }
}
