using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using brian.Components;
public class tempHealComp : MonoBehaviour
{
    public HealthEffector he;
    public float healVal;
    public bool heal;
    // Start is called before the first frame update
    void Start()
    {
        he = new HealthEffector();
        he.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (heal)
        {
            processHeal();
            heal = false;
        }

    }
    void processHeal()
    {
        he.affect(true, healVal, 1);
    }
}
