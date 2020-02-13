using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisScale : AudioSync
{
    public Vector3 beatSize;
    public Vector3 restSize;

    public override void onUpdate()
    {
        base.onUpdate();
        if (isBeat) return;
        transform.localScale = Vector3.Lerp(transform.localScale, restSize, restSmoothTime * Time.deltaTime);
    }
    public override void OnBeat()
    {
        base.OnBeat();
        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatSize);
    }
    private IEnumerator MoveToScale(Vector3 target)
    {
        Vector3 currScale = transform.localScale;
        Vector3 startScale = currScale;

        float timer = 0;

        while(currScale != target)
        {
            currScale = Vector3.Lerp(startScale, target, timer / timeToBeat);
            timer += Time.deltaTime;

            transform.localScale = currScale;
            yield return null;
        }
        isBeat = false;
    }

}
