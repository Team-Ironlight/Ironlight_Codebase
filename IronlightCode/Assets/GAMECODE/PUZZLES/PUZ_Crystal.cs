using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUZ_Crystal : MonoBehaviour, IHit
{
    private LineRenderer _ucLightRenderer;

    [SerializeField] private float _fRange = 10;
    private IHit _ciPreviousHit = null;

    [SerializeField] private bool _bAlwaysHasLight = false;

    private void Start()
    {
        _ucLightRenderer = GetComponent<LineRenderer>();

        if (!_bAlwaysHasLight)
            _ucLightRenderer.enabled = false;
    }

    private void Update()
    {
        if (_bAlwaysHasLight) HitWithLight(1);
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
            if (_ciPreviousHit != null)
            {
                _ciPreviousHit.ExitHitWithLight();
                _ciPreviousHit = other;
            }
        }
        // Hitting hitable
        else
        {
            // Started hitting something new
            if (_ciPreviousHit == null)
            {
                // Was not hitting something
                other.EnterHitWithLight(pAmount);
                _ciPreviousHit = other;
            }

            else if (other != _ciPreviousHit)
            {
                // Was hitting something
                other.EnterHitWithLight(pAmount);
                _ciPreviousHit.ExitHitWithLight();
                _ciPreviousHit = other;
            }

            // Update what is currently being hit
            other.HitWithLight(pAmount);
        }

        // Update lineRendereer
        _ucLightRenderer.SetPosition(1, Vector3.forward * (hit.distance == 0 ? _fRange : hit.distance));
    }

    public void EnterHitWithLight(float pAmount)
    {
        _ucLightRenderer.enabled = true;
    }

    public void ExitHitWithLight()
    {
        _ucLightRenderer.enabled = false;

        // Stop hitting what you are hitting
        if (_ciPreviousHit != null)
        {
            _ciPreviousHit.ExitHitWithLight();
            _ciPreviousHit = null;
        }
    }
}
