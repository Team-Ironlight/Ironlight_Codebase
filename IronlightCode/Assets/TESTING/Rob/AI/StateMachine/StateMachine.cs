using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private AI ai;
    public IState currentState;
    public IAttack currentAttack;
    public IMove currentMove;

    private void Awake()
    {
        //get ai
        ai = GetComponent<AI>();        
    }

    private void Start()
    {
        //sets starting state based on states this enemy has
        currentState = new None();
        Debug.Log("Current State: " + currentState.Name());
    }

    private void Update()
    {
        Run();

        //if(ai.GetisReady())
        //{
        //    Run();
        //}        
    }

    private void Run()
    {
        //check if AI has changed the state, exit and enter
        if (currentState != ai.CheckContainer(ai.GetContainer()))
        {
            currentState.Exit();
            currentState = ai.CheckContainer(ai.GetContainer());
            currentState.Enter();
        }
        //run execute of current state
        else
        {
            currentState.Execute(gameObject.transform);
        }
    }
}
