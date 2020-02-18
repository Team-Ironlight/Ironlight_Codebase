using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearRotation
{
    public static Vector3 AroundX(Vector3 v, float degree)
    {
        float rad = degree * Mathf.PI / 180f;

        return new Vector3(v.x,
                           Mathf.Cos(rad) * v.y - Mathf.Sin(rad) * v.z,
                           Mathf.Sin(rad) * v.y + Mathf.Cos(rad) * v.z);
    }

    public static Vector3 AroundY(Vector3 v, float degree)
    {
        float rad = degree * Mathf.PI / 180f;

        return new Vector3(Mathf.Cos(rad) * v.x - Mathf.Sin(rad) * v.z, 
                           v.y,
                           Mathf.Sin(rad) * v.x + Mathf.Cos(rad) * v.z);        
    }

    public static Vector3 AroundZ(Vector3 v, float degree)
    {
        float rad = degree * Mathf.PI / 180f;

        return new Vector3(Mathf.Cos(rad) * v.x - Mathf.Sin(rad) * v.y, 
                           Mathf.Sin(rad) * v.x + Mathf.Cos(rad) * v.y,
                           v.z);
    }
}
