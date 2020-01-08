using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName="Destruction/Hide Behind Effect")]
public class SYS_HideBehindEffect : SYS_DestructionSequence
{
	public GameObject Effect;
	public float DestroyOriginalAfterTime = 1f;

	public override IEnumerator SequenceCoroutine(MonoBehaviour runner)
	{
		Instantiate(Effect, runner.transform.position, runner.transform.rotation);
		yield return new WaitForSeconds(DestroyOriginalAfterTime);
		Destroy(runner.gameObject);
	}
}
