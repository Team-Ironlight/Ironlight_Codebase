// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/8/2020
using System.Collections;
using UnityEngine;

public abstract class SYS_DustScriptableObject : ScriptableObject
{
    public abstract IEnumerator DustCoroutine(MonoBehaviour runner);
}

