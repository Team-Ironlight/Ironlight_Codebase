using System.Collections;
using UnityEngine;

public abstract class SYS_DestructionSequence : ScriptableObject
{
	public abstract IEnumerator SequenceCoroutine(MonoBehaviour runner);
}
