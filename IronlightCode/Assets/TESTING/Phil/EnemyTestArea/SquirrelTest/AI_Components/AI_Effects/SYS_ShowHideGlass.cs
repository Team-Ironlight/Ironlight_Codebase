// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/13/2020
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/Effects/New Glass Effect")]
public class SYS_ShowHideGlass : SYS_GlassScriptableObject
{
    public GameObject Effect;
    public float DestroyOriginalAfterTime = 1f;

    public override IEnumerator GlassCoroutine(MonoBehaviour runner)
    {
        WaitForSeconds _waitTime = new WaitForSeconds(DestroyOriginalAfterTime);
        Instantiate(Effect, runner.transform.position, runner.transform.rotation);
        yield return _waitTime;
    }
}

