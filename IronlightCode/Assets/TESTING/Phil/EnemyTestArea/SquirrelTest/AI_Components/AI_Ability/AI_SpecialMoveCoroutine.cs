// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// ----------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AI_SpecialMoveCoroutine : ScriptableObject
{

    public abstract IEnumerator Rotate_Coroutine(MonoBehaviour runner, float minDistanceToAttack, float maxDistanceToAttack, bool isCharging);

}
