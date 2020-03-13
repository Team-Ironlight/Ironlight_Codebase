using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class R_BeamVisual : MonoBehaviour
{
    public GameObject beamStart = null;
    public GameObject beamLoop = null;

    public bool go = false;

    private void Update()
    {
        if (go)
        {
            PlayStart();
            go = false;
        }
    }

    public void PlayStart()
    {
        var vfx = beamStart.GetComponent<ParticleSystem>();
        vfx.Play();

        var vfxLoop = beamLoop.GetComponentsInChildren<ParticleSystem>();
        foreach(var fx in vfxLoop)
        {
            fx.time = 0;
            fx.Play();
        }
    }
}
