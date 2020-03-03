using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Viet.Components;
using brian.Components;

public class example : MonoBehaviour
{
    HealthComponent hp = null;
    HealthEffector hpEffector = null;
    DamageComp dmg = null;
    AbsorbComp health;

    public float dmgVal = 0;
    public float healVal = 0;

    // Start is called before the first frame update
    void Start()
    {
        hp = new HealthComponent();
        hp.Init(100);

        hpEffector = new HealthEffector();
        hpEffector.Init(hp);

        dmg = new DamageComp();
        dmg.Init(hpEffector);

        health = new AbsorbComp();
        health.Init(hpEffector);
    }

    // Update is called once per frame
    void Update()
    {
        //if (dmg != null)
        //{
        //    if (dmg.damageValue != dmgVal)
        //    {
        //        dmg.UpdateValues(dmgVal);
        //    }
        //    Debug.Log(dmg.damageValue);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        dmg.DoIt(dmgVal, 1);
    }

    private void OnTriggerExit(Collider other)
    {
        health.DoIt(healVal, 1);
    }
}