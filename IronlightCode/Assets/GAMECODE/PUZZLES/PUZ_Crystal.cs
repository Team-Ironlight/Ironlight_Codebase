using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUZ_Crystal : MonoBehaviour, IHit
{
    LineRenderer lightRenderer;

    private void Start()
    {
        lightRenderer = GetComponent<LineRenderer>();
        lightRenderer.enabled = false;
    }

    public void HitWithLight(float pAmount)
    {
        lightRenderer.enabled = true;

        RaycastHit hit;
    }
}
