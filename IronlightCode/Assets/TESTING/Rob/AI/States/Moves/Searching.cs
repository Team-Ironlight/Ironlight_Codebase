using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomRaycasts : IRState
{
    string name = "Searching";

    public override string Name()
    {
        return name;
    }

    private float rotationSpeed;    
    private float rotationAmount;
    private float rotationCount;
    private int rotationDirection;


    public override void Enter()
    {
        Debug.Log("Enter Searching");
        SetValues();
    }

    public override void Execute(Transform t)
    {
        Debug.Log("Execute Searching");
        //rotate
        if (rotationCount < Mathf.Abs(rotationAmount))
        {
            t.Rotate(0f, rotationSpeed * rotationDirection * Time.deltaTime, 0f);
            rotationCount += rotationSpeed * Time.deltaTime;
        }
        else
        {            
            SetValues();
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Searching");
    }    


    private void SetValues()
    {
        rotationSpeed = Random.Range(10f, 20f);
        rotationAmount = Random.Range(30f, 60f);
        rotationCount = 0f;

        do
        {
            rotationDirection = Random.Range(-1, 2);
        }
        while (rotationDirection == 0);

        //Debug.Log("New Rotation: " + rotationAmount + " " + rotationDirection);
    }
}
