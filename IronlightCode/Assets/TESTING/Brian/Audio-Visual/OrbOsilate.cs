using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbOsilate : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float oscillationRate = 1;
    [SerializeField] [Range(0, 1)] private float oscillationRange = 1;
    [SerializeField] private float upperHeightLimit = 10;
    [SerializeField] private float lowerHeightLimit = 1;
    [SerializeField] private float XAxisHighLimit = 2;
    [SerializeField] private float XAxisLowLimit = 1.5f;
    private void Update()
    {
        transform.position = ClampHeight((Vector3.up * Mathf.Cos(Time.time * oscillationRate) * ClampRange(oscillationRange)) + transform.position);
       // transform.position = ClampHeight((Vector3.left * Mathf.Cos(Time.time * oscillationRate) * ClampRange(oscillationRange)) + transform.position);
    }

    private float ClampRange(float value)
    {
        if (transform.position.y > upperHeightLimit)
            upperHeightLimit = transform.position.y;
        if (transform.position.y < lowerHeightLimit)
            lowerHeightLimit = transform.position.y - lowerHeightLimit;
        if (upperHeightLimit < lowerHeightLimit)
            upperHeightLimit = lowerHeightLimit + 0.1f;
        if (lowerHeightLimit > upperHeightLimit)
            lowerHeightLimit = upperHeightLimit + 0.1f;
        if (value != ((upperHeightLimit + lowerHeightLimit) / 2) - 0.25f)
            value = ((upperHeightLimit + lowerHeightLimit) / 2) - 0.25f;
        if (value != value * oscillationRange)
            value *= oscillationRange;
       //added lateral
        //if (transform.position.x > XAxisHighLimit)
        //    XAxisHighLimit = transform.position.x;
        //if (transform.position.x < XAxisLowLimit)
        //    XAxisLowLimit = transform.position.x - XAxisLowLimit;
        //if (XAxisHighLimit < XAxisLowLimit)
        //    XAxisHighLimit = XAxisLowLimit + 0.1f;
        //if (XAxisLowLimit > XAxisHighLimit)
        //    XAxisLowLimit = XAxisHighLimit + 0.1f;
        //if (value != ((XAxisHighLimit + XAxisLowLimit) / 2) - 0.25f)
        //    value = ((XAxisHighLimit + XAxisLowLimit) / 2) - 0.25f;
        //if (value != value * oscillationRange)
        //    value *= oscillationRange;
        value *= 0.01f;

        return value;
    }

    private Vector3 ClampHeight(Vector3 value)
    {
        if (value.y < lowerHeightLimit)
            value.y = lowerHeightLimit;
        if (value.y > upperHeightLimit)
            value.y = upperHeightLimit;
        //if (value.x < XAxisLowLimit)
        //    value.x = XAxisLowLimit;
        //if (value.x > XAxisHighLimit)
        //    value.x = XAxisHighLimit;

        return value;
    }
}
