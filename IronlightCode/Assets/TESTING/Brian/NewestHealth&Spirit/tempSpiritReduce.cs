using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tempSpiritReduce : MonoBehaviour
{
    spiritEffector sc;
    public float subVal;
    public bool drain;
    public bool gain;
    public float addVal;
    // Start is called before the first frame update
    void Start()
    {
        sc = new spiritEffector();
        sc.Init();
    }

    // Update is called once per frame
    void Update()
    {
       if (drain)
        {
            processDrain();
            drain = false;
        }
       if (gain)
        {
            processGain();
            gain = false;
        }
    }
    void processDrain()
    {
        sc.AffectSpirit(false, subVal, 1);
    }
    void processGain()
    {
        sc.AffectSpirit(true, addVal, 1);
    }
}
