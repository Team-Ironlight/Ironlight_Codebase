// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   03/03/2020
// ----------------------------------------------------------------------------
using UnityEngine;
using System.Collections;

public class Phil_ActionBase : MonoBehaviour
{
    public virtual void Act()
    {
        Debug.Log("Override ActionBase#Act!");
    }
}
