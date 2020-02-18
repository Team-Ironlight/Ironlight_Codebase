using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class None : IRState
{
    string name = "None";

    public override string Name()
    {
        return name;
    }


    public override void Enter()
    {
        Debug.Log("Enter");
    }

    public override void Execute(Transform t)
    {
        Debug.Log("None");
    }

    public override void Exit()
    {
        Debug.Log("Exit");
    }
}
