// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/8/2020
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI System/New DrilCloud Effect")]
public class SYS_ShowHideTendril : SYS_TendrilScriptableObject
{
    public GameObject Effect;
    public float DestroyOriginalAfterTime = 1f;

    public override IEnumerator DrilCloudCoroutine(MonoBehaviour runner)
    {
        Instantiate(Effect, runner.transform.position, runner.transform.rotation);
        yield return new WaitForSeconds(DestroyOriginalAfterTime);
    }
}