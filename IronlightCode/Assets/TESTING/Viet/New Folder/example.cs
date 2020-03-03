using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viet.Components;

public class example : MonoBehaviour
{
    DamageComp dmg = null;
    public float dmgVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        dmg = new DamageComp();
        dmg.Init(dmgVal);
    }

    // Update is called once per frame
    void Update()
    {
        if (dmg != null)
        {
            if (dmg.damageValue != dmgVal)
            {
                dmg.UpdateValues(dmgVal);
            }
            Debug.Log(dmg.damageValue);
        }
    }
}