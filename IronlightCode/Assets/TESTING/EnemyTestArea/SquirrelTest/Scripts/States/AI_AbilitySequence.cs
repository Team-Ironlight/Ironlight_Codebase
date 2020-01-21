//Programmer : Phil James
//Description :  Version 2 (January 16, 2020)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_AbilitySequence : ScriptableObject
{
    public abstract IEnumerator Attack1_Coroutine(MonoBehaviour runner);
}
