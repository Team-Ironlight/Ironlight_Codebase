using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForce : MonoBehaviour
{
    public float gravityStrength = -10f;
    public float gravityTotal;
    public float count;
    public bool isOn = false;

    private Coroutine c = null;


    public float GetGravity()
    {
        return gravityTotal;
    }

    public void Set(bool b)
    {
        if (b)
        {
            isOn = true;
            if (c == null)
            {
                c = StartCoroutine(Calculate());
            }

        }
        else
        {
            isOn = false;
            count = 0f;
            c = null;
        }
    }

    public void Reset()
    {
        StopCoroutine(c);
        c = null;   
        c = StartCoroutine(Calculate());
    }

    IEnumerator Calculate()
    {
        count = 0.0f;

        while (isOn)
        {
            count += Time.deltaTime;
            gravityTotal = 0.5f * gravityStrength * (count * count);
            yield return null;
        }
    }
}
