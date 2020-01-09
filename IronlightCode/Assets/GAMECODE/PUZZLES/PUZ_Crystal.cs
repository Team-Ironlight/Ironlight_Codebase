using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUZ_Crystal : MonoBehaviour, IHit
{
    LineRenderer lightRenderer;

    [SerializeField] private float _fRange = 10;
    private IHit previousHit = null;

    [SerializeField] private bool hasLight = false;

    private void Start()
    {
        lightRenderer = GetComponent<LineRenderer>();

        if (!hasLight)
            lightRenderer.enabled = false;
    }

    private void Update()
    {
        if (hasLight) HitWithLight(10);
    }

    public void HitWithLight(float pAmount)
    {
        // Check for if it
        RaycastHit hit;
        IHit other = null;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
            other = hit.collider.GetComponent<IHit>();

        // Is not hitting anything
        if (other == null)
        {
            // Stopped hitting something
            if (previousHit != null)
            {
                previousHit.ExitHitWithLight();
                previousHit = other;
            }
        }
        // Hitting hitable
        else
        {
            // Started hitting something new
            if (previousHit == null)
            {
                // Was not hitting something
                other.EnterHitWithLight(pAmount);
                previousHit = other;
            }

            else if (other != previousHit)
            {
                // Was hitting something
                other.EnterHitWithLight(pAmount);
                previousHit.ExitHitWithLight();
                previousHit = other;
            }

            // Update what is currently being hit
            other.HitWithLight(pAmount);
        }

        // Update lineRendereer
        lightRenderer.SetPosition(1, Vector3.forward * (hit.distance == 0 ? _fRange : hit.distance));

        if (hasLight) Debug.Log(hit.distance);
    }

    public void EnterHitWithLight(float pAmount)
    {
        lightRenderer.enabled = true;
    }

    public void ExitHitWithLight()
    {
        lightRenderer.enabled = false;

        // Stop hitting what you are hitting
        if (previousHit != null)
        {
            previousHit.ExitHitWithLight();
            previousHit = null;
        }
    }
}
