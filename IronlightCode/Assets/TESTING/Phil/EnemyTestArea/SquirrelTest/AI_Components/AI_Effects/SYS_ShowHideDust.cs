using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI System/New Dust Effect")]
public class SYS_ShowHideDust : SYS_DustScriptableObject
{
    public GameObject Effect;
    public float DestroyOriginalAfterTime = 1f;

    public override IEnumerator DustCoroutine(MonoBehaviour runner)
    {
        Instantiate(Effect, runner.transform.position, runner.transform.rotation);
        yield return new WaitForSeconds(DestroyOriginalAfterTime);  
    }
}