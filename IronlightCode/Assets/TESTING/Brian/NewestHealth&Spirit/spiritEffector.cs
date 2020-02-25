using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spiritEffector 
{
    SpiritComponent sc;
    [SerializeField]
    bool isChargeing;
    [SerializeField]
    bool isShooting;
    public float multiplyer;
    // Start is called before the first frame update
    public void Init()
    {
        sc = new SpiritComponent();
        sc.Init();
    }

    public void AffectSpirit(bool plusSpirit, float val, float multi)
    {
        multiplyer = multi;
        float value = val * multiplyer;

        if (!plusSpirit)
        {
            sc.subSpirit(value);
        }
        else
        {
            sc.addSpirit(value);
        }
        Debug.Log(sc.currSpirit);
    }
}
