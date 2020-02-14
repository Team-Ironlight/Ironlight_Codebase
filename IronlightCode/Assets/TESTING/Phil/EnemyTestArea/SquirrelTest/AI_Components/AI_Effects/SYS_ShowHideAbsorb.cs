// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/13/2020
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI System - by DonPhilifeh/Effects/New Absorb Effect")]
public class SYS_ShowHideAbsorb : SYS_AbsorbScriptableObject
{
    public GameObject Effect;
    public float DestroyOriginalAfterTime = 1f;

    public override IEnumerator AbsorbCoroutine(MonoBehaviour runner)
    {
        WaitForSeconds _waitTime = new WaitForSeconds(DestroyOriginalAfterTime);
        Instantiate(Effect, runner.transform.position, runner.transform.rotation);
        yield return _waitTime;
    }
}
