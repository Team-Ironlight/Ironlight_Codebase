using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCrystal : MonoBehaviour
{

    public bool activated;
    public float ActiveTimeStart;
    float CurrActiveTime;
    // Start is called before the first frame update
    void Start()
    {
        CurrActiveTime = ActiveTimeStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            CurrActiveTime -= Time.deltaTime;
            print("ActiveTime" + CurrActiveTime);
            if (CurrActiveTime <= 0)
            {
                activated = false;
                CurrActiveTime = ActiveTimeStart;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Orb"))
        {
            activated = true;
            print("Crystal Active");
        }
    }
}
