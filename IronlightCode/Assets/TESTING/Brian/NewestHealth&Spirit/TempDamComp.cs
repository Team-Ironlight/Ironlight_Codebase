using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;

public class TempDamComp: MonoBehaviour
{
    public HealthEffector he;
    public float damVal;
    public float healVal;
    public bool dmg;
    public bool heal;

    private void Start()
    {
        he = new HealthEffector();
        he.Init();
    }
    private void Update()
    {
        if (dmg)
        {
            processDam();
            dmg = false;
        }

        if (heal)
        {
            processHeal();
            heal = false;
        }
    }
    void processDam()
    {
        he.affect(false, damVal,1);

    }


    void processHeal()
    {
        he.affect(true, healVal, 1);
    }
}
