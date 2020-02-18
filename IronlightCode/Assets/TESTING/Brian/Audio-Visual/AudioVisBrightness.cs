using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisBrightness : AudioSync
{

    public Light Intensity;
    public float beatBright;
    public float restBright;

 
    public override void onUpdate()
    {
        base.onUpdate();
        if (isBeat) return;
        //transform.localScale = Vector3.Lerp(transform.localScale, restBright, restSmoothTime * Time.deltaTime);
        Intensity.intensity = Mathf.Lerp(Intensity.intensity, restBright, restSmoothTime * Time.deltaTime);
    }
    public override void OnBeat()
    {
        base.OnBeat();
        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatBright);
    }
    private IEnumerator MoveToScale(float target)
    {
        float currBright = Intensity.intensity;
        float startBright = currBright;

        float timer = 0;

        while (currBright != target)
        {
            currBright = Mathf.Lerp(startBright, target, timer / timeToBeat);
            timer += Time.deltaTime;

            Intensity.intensity = currBright;
            yield return null;
        }
        isBeat = false;
    }

}

