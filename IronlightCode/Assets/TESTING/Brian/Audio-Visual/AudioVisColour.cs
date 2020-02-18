using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisColour : AudioSync
{
    public Renderer rend;
    private Material mat;

    public Vector4 beatColour;
    public Vector4 restColour;


    private Vector4 _beat = Vector4.zero;
    private Vector4 _rest = Vector4.zero;
    public override void onUpdate()
    {
        base.onUpdate();
        if (isBeat) return;
        ConvertVal();
        rend.material.color = Vector4.Lerp(rend.material.color, restColour, restSmoothTime * Time.deltaTime);
    }
    public override void OnBeat()
    {
        base.OnBeat();
        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", beatColour);
    }
    private IEnumerator MoveToScale(Vector4 target)
    {
        Vector4 currColour = rend.material.color;
        Vector4 startColour = currColour;

        float timer = 0;

        while (currColour != target)
        {
            currColour = Vector4.Lerp(startColour, target, timer / timeToBeat);
            timer += Time.deltaTime;

            rend.material.color = currColour;
            yield return null;
        }
        isBeat = false;
    }
    void ConvertVal()
    {
        _beat.x = beatColour.x % 255;
        _beat.y = beatColour.y % 255;
        _beat.z = beatColour.z % 255;
        _beat.w = beatColour.w % 255;

        _rest.x = restColour.x % 255;
        _rest.y = restColour.y % 255;
        _rest.z = restColour.z % 255;
        _rest.w = restColour.w % 255;
    }

}
