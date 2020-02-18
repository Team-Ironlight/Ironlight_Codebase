using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeRattle : IRState
{
    public string name = "SnakeRattle";

    public override string Name()
    {
        return name;
    }



    public override void Enter()
    {
        
    }

    public override void Execute(Transform t)
    {
        Debug.Log("Rattle");
    }

    public override void Exit()
    {
        
    }    
}
