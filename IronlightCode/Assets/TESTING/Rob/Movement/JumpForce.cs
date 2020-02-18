using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    public float jumpStrength = 1f;
    public float jumpTotal;
    public float count = 0.0f;
    public bool isOn = false;
    private Coroutine c = null;

    public float GetJump()
    {
        return jumpTotal;
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
            count = 0.0f;
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
            jumpTotal = jumpStrength * count;
            yield return null;
        }
    }
}
