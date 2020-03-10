using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish.Components.Abstract
{

    //[System.Serializable]
    public abstract class dBaseComponent : ScriptableObject
    {
        public string valueKey = "";
        public virtual void Init()
        {
            Debug.Log("Fuck");
        }

        public virtual void Tick()
        {
            Debug.Log("Bitches");
        }
    }
}