// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/8/2020
using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "AI System/New RingHorizontal Effect")]
public class SYS_ShowHideRing : SYS_RingScriptableObject
{
    public GameObject Effect;
    public float DestroyOriginalAfterTime = 1f;

    public override IEnumerator RingHorizontalCoroutine(MonoBehaviour runner, Vector3 position)
    {
        Instantiate(Effect, position, runner.transform.rotation);
        yield return new WaitForSeconds(DestroyOriginalAfterTime);
    }
}
