// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/8/2020
using System.Collections;
using UnityEngine;

public abstract class SYS_RingScriptableObject : ScriptableObject
{
    public abstract IEnumerator RingHorizontalCoroutine(MonoBehaviour runner, Vector3 position);
}