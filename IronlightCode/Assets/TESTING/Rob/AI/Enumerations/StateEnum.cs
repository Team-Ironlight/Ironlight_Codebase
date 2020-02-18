using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//states
public enum State { None, Rotate, Searching, SnakeRattle, Projectile };

public static class StateEnum 
{
    //return state attached to gameobject passed in
    public static IState SetState(GameObject g, State s)
    {
        switch (s)
        {
            case State.None:
                return g.GetComponent<None>();
            case State.Rotate:
                return g.GetComponent<Rotating>();
            case State.Searching:
                return g.GetComponent<CustomRaycasts>();
            case State.SnakeRattle:
                return g.GetComponent<SnakeRattle>();
        }

        return null;
    }

    //return state based on State enum passed in
    public static IState CreateState(State s, ConditionContainer cc)
    {
        switch (s)
        {
            case State.None:
                return new None();
            case State.Rotate:
                return new Rotating(GameObject.Find("Player").transform);
            case State.Searching:
                return new CustomRaycasts();
            case State.SnakeRattle:
                return new SnakeRattle();
            case State.Projectile:
                //down cast first
                return new Projectile(cc.launcher, cc.projectile, cc.launchTime);
        }

        Debug.Log("Returning null");
        return null;
    }
}
