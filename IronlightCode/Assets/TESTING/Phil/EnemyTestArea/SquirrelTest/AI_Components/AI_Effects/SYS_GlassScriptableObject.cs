// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   02/13/2020 -ver 3
using System.Collections;
using UnityEngine;

public abstract class SYS_GlassScriptableObject : ScriptableObject
{
    public abstract IEnumerator GlassCoroutine(MonoBehaviour runner);
}
