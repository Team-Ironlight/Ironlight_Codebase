using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Components;

public class dmgAbsorbTester : MonoBehaviour
{
    public float damageAmount = 5;
    public float absorbAmount = 2;

    public dSpiritSystem spiritSystem = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spiritSystem.DRAIN.DoIt(10, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out dHealthSystem healthSystem))
        {
            healthSystem.DMG.DoIt(damageAmount, 1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //if(other.gameObject.TryGetComponent(out dHealthSystem healthSystem))
        //{
        //    healthSystem.ABS.DoIt(absorbAmount * Time.deltaTime, 1);
        //}

        if(other.gameObject.TryGetComponent(out dSpiritSystem _spirit))
        {
            spiritSystem = _spirit;

            spiritSystem.GAIN.DoIt(absorbAmount * Time.deltaTime, 1);
        }
    }
}
